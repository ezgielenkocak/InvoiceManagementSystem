using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.ApartmentDtos
{
    public class ApartmentAddDto:IDto
    {
        public string WhichBlock { get; set; }
        public bool ApartmentState { get; set; }
        public int ApartmentNo { get; set; }
        public int FloorNumber { get; set; }
        public string ApartmentSize { get; set; }
        public bool Status { get; set; }
    }
}
