using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.UserBillDtos
{
    public class UserBillListDto:IDto
    {
        public int Id { get; set; }
        //public int UserId { get; set; }
        //public int BillId { get; set; }
        public string UserDetails { get; set; }
        public string BillName { get; set; }
        public bool Status { get; set; }

    }
}
