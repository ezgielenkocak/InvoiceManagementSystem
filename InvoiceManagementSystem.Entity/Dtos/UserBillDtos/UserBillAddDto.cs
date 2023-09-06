using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.UserBillDtos
{
    public class UserBillAddDto:IDto
    {
        public int UserId { get; set; }
        public int BillId { get; set; }
        //public bool Status { get; set; }
    }
}
