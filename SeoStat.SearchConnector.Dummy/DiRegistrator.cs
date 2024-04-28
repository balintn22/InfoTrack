using Autofac;
using SeoStat.Domain;
using System.Diagnostics.CodeAnalysis;

namespace SeoStat.SearchConnector.Dummy;

[ExcludeFromCodeCoverage]
public static class DiRegistrator
{
    public static void UseDummySearch(this ContainerBuilder builder)
    {
        builder.RegisterType<DummySearchService>().As<ISearchService>().SingleInstance();
    }
}
