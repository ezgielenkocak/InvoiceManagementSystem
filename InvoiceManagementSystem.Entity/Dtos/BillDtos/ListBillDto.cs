using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace InvoiceManagementSystem.Entity.Dtos.BillDtos
{
    public class ListBillDto : IDto
    {
        public int Id { get; set; }
        public int? BillTypeId { get; set; }
        public string BillName { get; set; }
        public decimal? Price { get; set; }
        public DateTime? EndPaymentDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsBillPayment { get; set; }


    }
}
