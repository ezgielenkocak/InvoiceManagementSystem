using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.SessionDtos
{
    public class UsersSessionCheckDto:IDto
    {
        public int UserId { get; set; }
        public string? TokenString { get; set; }
        public string? UserAgent { get; set; }
        public string? Ip { get; set; }
        public string? SiteType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Email { get; set; }
    }
}
