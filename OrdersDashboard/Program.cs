using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrdersDashboard.Client.NorthwindServices;
using Syncfusion.Blazor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OrdersDashboard
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //Register Syncfusion license 
            Syncfusion.Licensing.SyncfusionLicenseProvider
                .RegisterLicense("NTA2NzIzQDMxMzkyZTMyMmUzMGFLSXF5dE8vWS95U1hzbmFTcXZZS2hQa0hZVkJoZDE3cnk5VS8wMWZBQ1U9");

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<ProductsService>();
            builder.Services.AddScoped<StatisticsService>();

            builder.Services.AddSyncfusionBlazor();

            await builder.Build().RunAsync();
        }
    }
}
