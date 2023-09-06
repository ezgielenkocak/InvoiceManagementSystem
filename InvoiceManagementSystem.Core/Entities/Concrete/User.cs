using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Core.Entities.Concrete
{
   public  class User:IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? TcNo { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool? Status { get; set; }

        public byte[]? PasswordSalt { get; set; }
        public byte[]? PasswordHash { get; set; }
        public string SecurityCode { get; set; }
        public DateTime? RegistrationDate { get; set; }

    }
}
