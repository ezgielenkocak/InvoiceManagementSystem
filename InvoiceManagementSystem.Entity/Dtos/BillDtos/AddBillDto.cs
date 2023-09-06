using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.BillDtos
{
    public class AddBillDto : IDto
    {
        public int BillTypeId { get; set; }
        public decimal Price { get; set; }
        public DateTime EndPaymentDate { get; set; }


    }
}
