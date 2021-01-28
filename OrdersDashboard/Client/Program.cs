using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrdersDashboard.Client.NorthwindServices;
using Syncfusion.Blazor;
using OrdersDashboard.Client.Spinner;
using Microsoft.AspNetCore.Components;

// https://bipinpaul.com/posts/display-spinner-on-each-api-call-automatically-in-blazor

namespace OrdersDashboard.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //Register Syncfusion license 
            Syncfusion.Licensing.SyncfusionLicenseProvider
                .RegisterLicense("MzgyOTI5QDMxMzgyZTM0MmUzMGJQVnlFd1ljMFgxWS9DUGFlWjJVdHhHY3dqVkhEbXB4OTNpem1XTFlBd1E9");


            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddScoped<SpinnerService>();

            builder.Services.AddScoped<SpinnerHttpMessageHandler>();


            builder.Services.AddScoped(s =>
            {
                var accessTokenHandler = s.GetRequiredService<SpinnerHttpMessageHandler>();

                accessTokenHandler.InnerHandler = new HttpClientHandler();
                var uriHelper = s.GetRequiredService<NavigationManager>();
                return new HttpClient(accessTokenHandler)
                {
                    BaseAddress = new Uri(uriHelper.BaseUri)
                };
            });

            builder.Services.AddScoped<ProductsService>();
            builder.Services.AddScoped<StatisticsService>();

            builder.Services.AddSyncfusionBlazor();

            await builder.Build().RunAsync();
        }
    }
}
