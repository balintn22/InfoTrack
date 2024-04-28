using System;
using System.Collections.Generic;

namespace SeoStat.Domain;

public interface ISeoTester
{
    /// <summary>
    /// Tests a search engine query to find out where a given url is returned in the result set.
    /// Returns the hit indexes and also stores them in a database repo.
    /// </summary>
    /// <param name="expectedUrl">URL we expect to find in the search result set.</param>
    /// <param name="searchString">Search string that is passed to a search engine. Usually a space delimited list of search words.</param>
    /// <returns>
    /// List of integers, representing the 1-based position of the result set items where the expected URL was returned.
    /// Contains at most 100 items.
    /// May contain 0 items if the expected URL is not found in ny of the result items.
    /// May not be null.
    /// </returns>
    List<int> TestSearch(string expectedUrl, string searchString);
}

public class SeoTester : ISeoTester
{
    private ISeoStatRepo _repo;
    private ISearchService _search;

    public SeoTester(
        ISeoStatRepo repo,
        ISearchService search)
    {
        _repo = repo;
        _search = search;
    }

    public List<int> TestSearch(string expectedUrl, string searchString)
    {
        var hitList = _search.TestHitIndexes(expectedUrl, searchString);
        var now = DateTime.Now;
        _repo.Insert(new SeoStatMeasurement(
            searchString: searchString,
            url: expectedUrl,
            timestamp: now,
            hitIndexes: hitList));

        return hitList;
    }
}
