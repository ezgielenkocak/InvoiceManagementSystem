using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.ApartmentDtos
{
    public class AddMultipleApartmentDto:IDto
    {
        public ApartmentAddDto[] Apartments { get; set; }
        public string Token { get; set; }
    }
}
