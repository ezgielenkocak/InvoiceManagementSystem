using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.AuthDtos
{
    public class SecuritiesCode:IDto
    {
        //public DateTime? ExpireDate { get; set; }
        public string SendToEmail { get; set; }
    }
}
