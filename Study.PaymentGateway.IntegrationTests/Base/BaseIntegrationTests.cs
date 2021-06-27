using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Study.PaymentGateway.API;

namespace Study.PaymentGateway.IntegrationTests.Base
{
    public class BaseIntegrationTests : WebApplicationFactory<Startup>
    {
        public IHost HostWeb { get; set; }

        private readonly string applicationSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), @".\Configuration\API\appsettings.integrationtests.json");

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, conf) =>
            {
                conf.AddJsonFile(applicationSettingsPath);
            });

            //  builder.UseStartup<Startup>();
        }

        /* protected override TestServer CreateServer(IWebHostBuilder builder)
         {
             return base.CreateServer(builder);
         }*/

        protected override IHost CreateHost(IHostBuilder builder)
        {
            HostWeb = builder.Build();

            HostWeb.Start();

            return HostWeb;
        }
    }
}