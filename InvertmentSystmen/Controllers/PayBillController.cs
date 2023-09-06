using Braintree;
using InvoiceManagementSystem.BLL.Abstract;
using InvoiceManagementSystem.Core.Result;
using InvoiceManagementSystem.Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace InvoiceManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayBillController : ControllerBase
    {
        private readonly IBraintreeService _braintreeService;
        private readonly IConfiguration _configuration;
        public PayBillController(IBraintreeService braintreeService, IConfiguration configuration)
        {
            _braintreeService = braintreeService;
            _configuration = configuration;
        }

        [HttpPost("createbraintoken")]
        public object CreateToken()
        {
            var gateway=_braintreeService.GetGateway();
            var clientToken = gateway.ClientToken.Generate();
            return clientToken;

        }
        [HttpPost("paybill")]
        public object PayBill()
        {
            var configuration = new BraintreeGateway
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = _configuration.GetValue<string>("BraintreeGateway:MerchantId"),
                PublicKey = _configuration.GetValue<string>("BraintreeGateway:PublicKey"),
                PrivateKey = _configuration.GetValue<string>("BraintreeGateway:PrivateKey")

            };

            var request = new TransactionRequest
            {
                Amount = 100M,
                PaymentMethodNonce = "the_nonce_from_the_client",
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true,
                }
            };

            Result<Transaction> result = configuration.Transaction.Sale(request);
            return result;
        }

    }
}
