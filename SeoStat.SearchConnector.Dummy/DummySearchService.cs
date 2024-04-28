using SeoStat.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeoStat.SearchConnector.Dummy
{
    /// <summary>
    /// Implements a dummy search connector returning a random sets of hit indexes.
    /// </summary>
    public class DummySearchService : ISearchService
    {
        public List<int> TestHitIndexes(string url, string searchString)
        {
            int maxNumberOfHits = 10;
            int hitCount = Random.Shared.Next(maxNumberOfHits);
            List<int> ret = new();
            for (int i = 0; i < hitCount; i++)
                ret.Add(Random.Shared.Next(100));

            return ret.Distinct().ToList();
        }
    }
}
