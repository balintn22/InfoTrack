using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.Extensions.Logging;
using SeoStat.SearchConnector.Google.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;

namespace SeoStat.SearchConnector.Google.PageWrappers;

public interface IGoogleSearchResultsPageWrapper
{
    Task<List<GoogleSearchResult>> GetSearchResultsAsync(string searchString);
}

public class GoogleSearchResultsPageWrapper : IGoogleSearchResultsPageWrapper
{
    private readonly IHtmlDocGetter _htmlPageGetter;
    private readonly ILogger<GoogleSearchResultsPageWrapper> _logger;
    private UrlEncoder _urlEncoder;

    public GoogleSearchResultsPageWrapper(
        IHtmlDocGetter htmlPageGetter,
        ILogger<GoogleSearchResultsPageWrapper> logger)
    {
        _htmlPageGetter = htmlPageGetter;
        _logger = logger;
        _urlEncoder = UrlEncoder.Default;
    }

    public async Task<List<GoogleSearchResult>> GetSearchResultsAsync(string searchString)
    {
        var htmlDoc = await _htmlPageGetter.GetAsync(
            $"https://www.google.com/search?q={_urlEncoder.Encode(searchString)}",
            expectedPageTitle: null!);

        var resultElements = GetResultHtmlElements(searchString);

        // TODO: Populate
        return new List<GoogleSearchResult>();
    }

    internal List<IHtmlAnchorElement> GetResultHtmlElements(string searchString)
    {
        string googleBaseUrl = "https://www.google.com/search";
        string searchUrl = $"{googleBaseUrl}?q={HttpUtility.UrlEncode(searchString)}";

        var config = Configuration.Default.WithDefaultLoader().WithDefaultCookies();
        var browsingContext = BrowsingContext.New(config);
        browsingContext.OpenAsync(searchUrl).GetAwaiter().GetResult();
        IDocument? doc = browsingContext?.Current?.Document;
        if(doc?.Title == "Before you continue to Google Search")
        {
            // The cookie accept form was displayed
            // Submit using the [Accept all] button
            IHtmlFormElement? form = doc?.Forms?.FirstOrDefault();
            List<IHtmlInputElement>? acceptAllButtons = doc?.All
                ?.Where(m => m.LocalName == "input" && (m as IHtmlInputElement).Type == "submit" && (m as IHtmlInputElement).Value == "Accept all")
                ?.Select(m => m as IHtmlInputElement)
                ?.ToList();
            IHtmlInputElement? acceptAllButton = acceptAllButtons?.Skip(1).First();

            if (form == null || acceptAllButton == null)
                return new List<IHtmlAnchorElement>();

            doc = form.SubmitAsync(acceptAllButton).GetAwaiter().GetResult();
        }

        if (!(doc?.Title?.EndsWith(" - Google Search") ?? false))
            return new List<IHtmlAnchorElement>();

        List<IHtmlAnchorElement> ret = doc.Anchors.ToList();
        // PROBLEM: Google search uses Ajax to populate search result, and AngleSharp does not support Ajax execution.
        // Solution would be to research appropriate tool and replace AngleSharp.
        return ret;
    }
}
