using System;
using System.Collections.Generic;

namespace SeoStat.Domain;

public class SeoStatMeasurement
{
    public string SearchString { get; set; }
    public string Url { get; set; }
    public DateTime Timestamp { get; set; }
    public List<int> HitIndexes { get; set; }

    public SeoStatMeasurement(
        string searchString,
        string url,
        DateTime timestamp,
        List<int> hitIndexes)
    {
        SearchString = searchString;
        Url = url;
        Timestamp = timestamp;
        HitIndexes = hitIndexes;
    }
}
