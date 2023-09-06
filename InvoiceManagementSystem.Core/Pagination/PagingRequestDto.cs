using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Core.Pagination
{
    public class PagingRequestDto:IDto
    {
        private int page;
        private int size;

        public int Page { get { return page; } set { page=value <= 0 ? 0 : value; } }
        public int Size { get { return page; } set { page = value <= 0 ? 0 : value; } }

    }
}
