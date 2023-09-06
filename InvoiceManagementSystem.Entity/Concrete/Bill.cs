using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Concrete
{
    public class Bill:IEntity
    {
        public int Id { get; set; }
        public int? BillTypeId { get; set; }
        public decimal? BillPrice { get; set; }
        public DateTime? EndPaymentDate { get; set; }
        public bool? IsBillPayment { get; set; }
        public DateTime? CreatedDate { get; set; }


    }
}
