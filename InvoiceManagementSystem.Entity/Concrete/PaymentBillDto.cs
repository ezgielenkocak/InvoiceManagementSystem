using Braintree;
using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Concrete
{
    public class PaymentBillDto:IDto
    {
        public CreditCardOptionsRequest Details { get; set; }
    }
}
