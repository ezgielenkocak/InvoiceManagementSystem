using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos
{
   public class ChangePasswordWithDto :IDto
    {
        public string Token { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordClone { get; set; }
        public string SecurityCode { get; set; }

    }
}
