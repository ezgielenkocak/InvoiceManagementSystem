using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Core.Pagination
{
    public class PagingResponseDto<T>:IDto
        where T:class, IEntity, new()
    {
        public List<T> Data { get; set; }
        public PagingDto Page { get; set; }
    }
}
