using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.BillDtos
{
    public class AddMultipleBillDto:IDto
    {
        public AddBillDto[] Bills { get; set; }

    }
}
