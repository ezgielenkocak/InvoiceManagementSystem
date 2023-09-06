using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.UserApartmentDtos
{
    public class UserApartmentAddMultipleDto:IDto
    {
        public UserApartmentAddDto[] UserApartmentAddDtos { get; set; }
    }
}
