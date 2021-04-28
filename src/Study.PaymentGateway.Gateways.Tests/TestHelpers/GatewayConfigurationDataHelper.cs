using System.Collections.Generic;
using Study.PaymentGateway.Gateways.Configuration;
using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Gateways.Tests.TestHelpers
{
    public static class GatewayConfigurationDataHelper
    {
        public static string loginURI = "http://domain.com/login";
        public static string executePaymentURI = "http://domain.com/payment";

        public static GatewayConfiguration GetGatewayConfiguration()
        {
            var gatewayConfiguration = new GatewayConfiguration();

            gatewayConfiguration.BankAPIs.Add(GetBankAPI());

            return gatewayConfiguration;
        }

        public static BankAPI GetBankAPI()
        {
            var bankAPI = new BankAPI();

            bankAPI.ActionUris.AddRange(GetAllActionUris());
            bankAPI.Code = BankCodeEnum.Visa;
            bankAPI.Name = "Visa";

            return bankAPI;
        }

        public static List<ActionUris> GetAllActionUris()
        {
            var actionUris = new List<ActionUris>();

            var loginActionUris = new ActionUris();
            loginActionUris.Action = Shared.Enums.GatewayActionsEnum.Login;
            loginActionUris.HttpVerb = "POST";
            loginActionUris.URI = loginURI;

            actionUris.Add(loginActionUris);

            var executeActionUris = new ActionUris();
            executeActionUris.Action = Shared.Enums.GatewayActionsEnum.ProcessPayment;
            executeActionUris.HttpVerb = "GET";
            executeActionUris.URI = executePaymentURI;

            actionUris.Add(executeActionUris);

            return actionUris;
        }
    }
}