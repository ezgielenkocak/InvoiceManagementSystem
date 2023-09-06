using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.UserDtos
{
    public class UserAddDto:IDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string TcNo { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool HaveaCar { get; set; }
        //public bool Status { get; set; }

    }
}
