using Autofac;
using SeoStat.Domain;
using System.Diagnostics.CodeAnalysis;

namespace SeoStat.Repo.Dummy;

[ExcludeFromCodeCoverage]
public static class DiRegistrator
{
    public static void UseDummyRepo(this ContainerBuilder builder)
    {
        builder.RegisterType<DummyRepo>().As<ISeoStatRepo>().SingleInstance();
    }
}
