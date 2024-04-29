using SeoStat.Domain;
using SeoStat.SearchConnector.Google.PageWrappers;
using System.Collections.Generic;

namespace SeoStat.SearchConnector.Google;

public class GoogleSearchService : ISearchService
{
    private readonly IGoogleSearchResultsPageWrapper _pageWrapper;

    public GoogleSearchService(
        IGoogleSearchResultsPageWrapper pageWrapper)
    {
        _pageWrapper = pageWrapper;
    }

    public List<int> TestHitIndexes(string url, string searchString)
    {
        var searchResults = _pageWrapper.GetSearchResultsAsync(searchString).GetAwaiter().GetResult();

        // TODO: Find the url in hit results

        return new List<int>();
    }
}
