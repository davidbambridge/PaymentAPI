using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AcquiringBankAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(ILogger<PaymentController> logger)
        {
            _logger = logger;
        }
        
        [HttpPost]
        public PaymentResult ProcessPayment()
        {
            Random random = new Random();
            PaymentResult result = new PaymentResult()
            {
                PaymentSucceeded = random.NextBool(),
                PaymentId = Guid.NewGuid().ToString()
            };

            return result;
        }


      

    }
}
