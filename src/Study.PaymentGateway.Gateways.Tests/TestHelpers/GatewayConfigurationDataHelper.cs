using System.Collections.Generic;
using Study.PaymentGateway.Gateways.Configuration;
using Study.PaymentGateway.Shared.Enums;

namespace Study.PaymentGateway.Gateways.Tests.TestHelpers
{
    public static class GatewayConfigurationDataHelper
    {
        public static string loginURI = "api/login";
        public static string executePaymentURI = "api/payment";

        public static GatewayConfiguration GetGatewayConfiguration()
        {
            var gatewayConfiguration = new GatewayConfiguration();

            gatewayConfiguration.BankAPIs.Add(GetBankAPI());

            return gatewayConfiguration;
        }

        public static BankAPI GetBankAPI()
        {
            var bankAPI = new BankAPI();

            bankAPI.User = "test";
            bankAPI.Password = "123";
            bankAPI.Login = "api/login";
            bankAPI.ExecutePayment = "api/executepayment";
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
            executeActionUris.HttpVerb = "POST";
            executeActionUris.URI = executePaymentURI;

            actionUris.Add(executeActionUris);

            return actionUris;
        }
    }
}