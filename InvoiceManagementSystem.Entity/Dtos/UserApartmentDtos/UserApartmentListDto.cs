using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.UserApartmentDtos
{
    public class UserApartmentListDto:IDto
    {
        //public int Id { get; set; }
        //public int UserId { get; set; }
        //public int ApartmentId { get; set; }
        public int ApartmentNo { get; set; }
        public int? FloorNumber { get; set; }
        public string UserDetails { get; set; }
        public string BlockName { get; set; }
    }
}
