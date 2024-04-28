namespace SeoStat.Domain;

public interface ISeoStatRepo
{
    /// <summary>
    /// Adds a measurement to the data store.
    /// </summary>
    /// <param name="item"></param>
    void Insert(SeoStatMeasurement item);

    /// <summary>
    /// Gets the total number of measurements taken so far.
    /// </summary>
    /// <returns></returns>
    int ItemCount();
}
