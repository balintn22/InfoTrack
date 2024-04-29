using AngleSharp;
using AngleSharp.Dom;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeoStat.SearchConnector.Google;

public interface IHtmlDocGetter
{
    /// <summary>
    /// Gets an AngleSharp IDocument representing the HTML page.
    /// </summary>
    /// <param name="url"></param>
    /// <param name="expectedPageTitle">
    /// If provided, enforces page title match, throwing an expection if no match.
    /// Set to null not to enforce page title.
    /// </param>
    Task<IDocument> GetAsync(string url, string expectedPageTitle);
}

/// <summary>
/// Implements a generic HTML page getter.
/// </summary>
public class HtmlDocGetter : IHtmlDocGetter
{
    private readonly HttpClient _httpClient;

    public HtmlDocGetter(
        HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IDocument> GetAsync(string url, string expectedPageTitle)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to get HTML IDocument from {url}.", null, response.StatusCode);

        string htmlContent = await response.Content.ReadAsStringAsync();

        // Use AngleSharp to parse the XML DOM
        var config = Configuration.Default;
        var context = BrowsingContext.New(config);
        IDocument doc = await context.OpenAsync(req => req.Content(htmlContent));

        if (expectedPageTitle != null && doc.Title != expectedPageTitle)
        {
            throw new HttpRequestException(
                $"Downloaded HTML page title does not match expected '{expectedPageTitle}'. " +
                $"It was instead '{doc.Title}'.");
        }

        return doc;
    }
}
