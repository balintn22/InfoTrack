using Autofac;
using SeoStat.Domain;
using SeoStat.SearchConnector.Google.PageWrappers;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

namespace SeoStat.SearchConnector.Google;

[ExcludeFromCodeCoverage]
public static class DiRegistrator
{
    public static void UseGoogleSearch(this ContainerBuilder builder)
    {
        builder.RegisterType<HtmlDocGetter>().As<IHtmlDocGetter>().SingleInstance();
        builder.RegisterType<GoogleSearchResultsPageWrapper>().As<IGoogleSearchResultsPageWrapper>().SingleInstance();
        builder.RegisterType<GoogleSearchService>().As<ISearchService>().SingleInstance();
        builder.RegisterInstance(new HttpClient()).As<HttpClient>();
    }
}
