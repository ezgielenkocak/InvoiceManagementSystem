using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.UserDtos
{
    public class UserRegisterAdminstratorDto:IDto
    {
        public string? Token { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //public string Password { get; set; }
        public string? IdentityNo { get; set; }

    }
}
