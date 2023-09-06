using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.UserDtos
{
    public class SecuritiesResponseDto:IDto
    {
        public string Token { get; set; }
        public string SecurityCode { get; set; }
        public string Email { get; set; }
        public List<SecuritySystem> SecuritySystems { get; set; }
    }

    public class SecuritySystem
    {
        public string SecurityType { get; set; }
        public string SecurityCode { get; set; }
        public bool Status { get; set; }

    }
}
