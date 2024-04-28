using Autofac;
using System.Diagnostics.CodeAnalysis;

namespace SeoStat.Domain;

[ExcludeFromCodeCoverage]
public static class DiRegistrator
{
    public static void UseSeoStatDomain(this ContainerBuilder builder)
    {
        builder.RegisterType<SeoTester>().As<ISeoTester>();
    }
}
