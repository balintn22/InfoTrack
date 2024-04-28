using SeoStat.Domain;
using System.Collections.Generic;

namespace SeoStat.Repo.Dummy;

/// <summary>
/// Implements a data repo with volatile, in memory storage.
/// </summary>
public class DummyRepo : ISeoStatRepo
{
    private List<SeoStatMeasurement> _dataStore = new List<SeoStatMeasurement>();

    public void Insert(SeoStatMeasurement measurement)
    {
        _dataStore.Add(measurement);
    }

    public int ItemCount() => _dataStore.Count;
}
