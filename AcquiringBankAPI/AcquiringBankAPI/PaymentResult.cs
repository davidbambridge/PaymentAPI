using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcquiringBankAPI
{
    public class PaymentResult
    {
        public bool PaymentSucceeded { get; set; }
        public string PaymentId { get; set; }
    }
}
