using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeoStat.Domain;

public interface ISearchService
{
    /// <summary>
    /// Executes a search query against a search service to determine where in the
    /// hit list a given URL is found.
    /// </summary>
    /// <param name="searchString">Text to search for.</param>
    /// <param name="url">URL expected to show up on the hit list.</param>
    /// <returns>1-based indexes of the search result hits where the URL was returned 
    /// or an emtpy list if it was not returned at all.</returns>
    List<int> TestHitIndexes(string url, string searchString);

    // TODO: Search is not CPU-bound, implement async search
    // Task<List<int>> TestHitIndexes(string url, string searchString);
}
