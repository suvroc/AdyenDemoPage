using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Adyen;
using Adyen.Service;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdyenDemo
{
    public class CanaryController : Controller
    {
        private const string ApiKey = "AQEnhmfuXNWTK0Qc+iSSsUU7pOuORp8cWMqonOdnuRgU7Hfyw6B2fqcUEMFdWw2+5HzctViMSCJMYAc=-2arc2+yjKT6xlcQC9fglswHZQswrdnv8Mqk3bIBJk10=-W;XP2t{QD6:rENY2";
        private const string MerchantAccount = "BCAccount159ECOM";

        // GET: /<controller>/
        [Route("api/canary")]
        public IActionResult Index()
        {
            return Ok("It works!");
        }

        [Route("api/paymentMethods")]
        public IActionResult PaymetMethods(PaymentMethodsParameters model)
        {
            Checkout checkout = CreateCheckut();
            var amount = new Adyen.Model.Checkout.Amount(model.CurrencyCode, model.Amount);
            var paymentMethodsRequest = new Adyen.Model.Checkout.PaymentMethodsRequest(MerchantAccount: MerchantAccount)
            {
                Amount = amount,
                Channel = Adyen.Model.Checkout.PaymentMethodsRequest.ChannelEnum.Web
            };
            if (!string.IsNullOrWhiteSpace(model.CountryCode))
            {
                paymentMethodsRequest.CountryCode = model.CountryCode;
            }
            var result = checkout.PaymentMethods(paymentMethodsRequest);

            return Json(result);
        }

        private static Checkout CreateCheckut()
        {
            var client = new Client(ApiKey, Adyen.Model.Enum.Environment.Test);

            var checkout = new Checkout(client);
            return checkout;
        }

        [HttpPost("api/payment")]
        public IActionResult PaymetMethods(PaymentModel data)
        {
            Checkout checkout = CreateCheckut();

            var paymentsRequest = new Adyen.Model.Checkout.PaymentRequest
            {
                Reference = Guid.NewGuid().ToString(),
                Amount = new Adyen.Model.Checkout.Amount(data.Currency, data.Amount),

                ReturnUrl = Request.Host +  @"/SuccessPage",
                MerchantAccount = MerchantAccount,
                PaymentMethod = data.PaymentMethodDetails
            };

            var paymentResponse = checkout.Payments(paymentsRequest);

            return Json(paymentResponse);
        }

        [HttpPost("api/payment/details")]
        public IActionResult PaymetMethodsDetails(Adyen.Model.Checkout.PaymentsDetailsRequest data)
        {
            Checkout checkout = CreateCheckut();

            var paymentsDetailsRequest = data;
            var paymentResponse = checkout.PaymentDetails(paymentsDetailsRequest);

            return Json(paymentResponse);
        }
    }

    public class PaymentMethodsParameters
    {
        public string CountryCode { get; set; }
        public string CurrencyCode { get; set; }
        public long Amount { get; set; }
    }

    public class PaymentModel
    {
        public Adyen.Model.Checkout.DefaultPaymentMethodDetails PaymentMethodDetails { get; set; }
        public string Currency { get; set; }
        public long Amount { get; set; }
    }
}
