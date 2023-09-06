using Braintree;
using InvoiceManagementSystem.BLL.Abstract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.BLL.Concrete
{
    public class BraingTreeManager : IBraintreeService
    {
        private readonly IConfiguration _config;

        public BraingTreeManager(IConfiguration config)
        {
            _config = config;
        }
        // appsettings.json dosyasından anahtarımıza erişmek için IConfiguration(_config) arayüzünü kullandık .
        public IBraintreeGateway CreateGateway()
        {
            var newGateway = new BraintreeGateway()
            {
                Environment=Braintree.Environment.SANDBOX,
                MerchantId=_config.GetValue<string>("BraintreeGateway:MerchantId"),
                PublicKey=_config.GetValue<string>("BraintreeGateway:PublicKey"),
                PrivateKey=_config.GetValue<string>("BraintreeGateway:PrivateKey")
            };
            
            return newGateway;
        }

        public IBraintreeGateway GetGateway()
        {
           return CreateGateway();
        }
    }
}
