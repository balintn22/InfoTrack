namespace SeoStat.SearchConnector.Google.Models;

public class GoogleSearchResult
{
    public string Url { get; set; }

    public GoogleSearchResult(string url)
    {
        Url = url;
    }
}
