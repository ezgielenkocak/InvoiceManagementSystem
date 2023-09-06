using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Core.Security
{
    public class SessionAddDto:IDto
    {
        public int UserId { get; set; }
        public string TokenString { get; set; }
        public string SiteType { get; set; }
        public string UserAgent { get; set; }
        public string Ip { get; set; }
    }
}
