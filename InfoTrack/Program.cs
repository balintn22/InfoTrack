using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SeoStat.Domain;
using SeoStat.Repo.Dummy;
using SeoStat.SearchConnector.Dummy;
using SeoStat.SearchConnector.Google;
using SeoStat.UI.Mvc;

namespace InfoTrack;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();

        // Register types with Autofac so that we can implement interceptors
        // To do that, we'll need a ref to Autofac.Extras.DynamicProxy as well
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(afBuilder =>
            {
                // Each library provides means to register its DI services. Call them to register those services.
                // If implementing some for real, switch to those implementations.
                //afBuilder.UseDummySearch();
                afBuilder.UseGoogleSearch();
                afBuilder.UseDummyRepo();
                afBuilder.UseSeoStatDomain();
            });

        builder.Services.AddSingleton<ISeoStatRepo, DummyRepo>();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseMiddleware<GlobalExceptionHandler>();
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapRazorPages();

        app.Run();
    }
}
