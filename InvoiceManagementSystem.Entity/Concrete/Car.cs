using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Concrete
{
    public class Car:IEntity
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string PlateNumber { get; set; }
        public string Color { get; set; }
        public bool IsTheCarLpg { get; set; }
    }
}
