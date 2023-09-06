using InvoiceManagementSystem.Core.Entities;
using InvoiceManagementSystem.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.SessionDtos
{
    public class SessionCheckResponseWithUserDto:IDto
    {
        public bool Success { get; set; }
        public User  User  { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
