using InvoiceManagementSystem.Core.Entities;
using InvoiceManagementSystem.Entity.Dtos.PagingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.BillDtos
{
    public class BillGetlistFilterDto:IDto
    {
        public string Token { get; set; }
        public string Search { get; set; }
        public PagingFilterDto PagingFilter { get; set; }
    }
}
