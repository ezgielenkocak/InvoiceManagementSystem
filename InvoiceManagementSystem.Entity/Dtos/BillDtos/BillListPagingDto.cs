using InvoiceManagementSystem.Core.Entities;
using InvoiceManagementSystem.Core.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.BillDtos
{
    public class BillListPagingDto:IDto
    {
        public List<ListBillDto> Data { get; set; }
        public PagingDto Paging { get; set; }
    }
}
