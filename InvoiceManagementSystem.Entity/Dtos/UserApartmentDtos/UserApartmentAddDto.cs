using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.UserApartmentDtos
{
    public class UserApartmentAddDto : IDto
    {
        public int UserId { get; set; }
        public int ApartmentId { get; set; }
    }
}
