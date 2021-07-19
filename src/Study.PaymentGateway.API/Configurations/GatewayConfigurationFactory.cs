using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services.GatewayConfig;
using Study.PaymentGateway.Gateways.Configuration;

namespace Study.PaymentGateway.API.Configurations
{
    public static class GatewayConfigurationFactory
    {
        public static void SetGatewayConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            var bankAPIs = configuration.GetSection("BankAPIs").Get<List<BankAPI>>();
            string json = JsonConvert.SerializeObject(bankAPIs);

            var bankAPIs1 = configuration.GetSection("ActionUris").Get<List<ActionUris>>();

            var ActionUris = configuration.GetSection("BankAPI:ActionUris");

            var gatewayconfig = new GatewayConfiguration();

            foreach (var bankApi in bankAPIs)
            {
                gatewayconfig.BankAPIs.Add(bankApi);
            }

            // services.Configure<IGatewayConfiguration>(gatewayconfig);

            services.AddSingleton<IGatewayConfiguration>(sp => gatewayconfig);
        }
    }
}