using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.PermissionDtos
{
    public class PermissionCheckDto:IDto
    {
        public string Token { get; set; }
        public string Permission { get; set; }
        public int UserId { get; set; }
    }
}
