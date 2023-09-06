using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos
{
    public class RegisterDto:IDto
    {
        public string Email { get; set; }
        public string TcNo { get; set; }
        public string Password { get; set; }
        public string PasswordCehck { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
    }
}
