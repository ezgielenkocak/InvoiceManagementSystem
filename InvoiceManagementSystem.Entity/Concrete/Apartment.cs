﻿using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Concrete
{
    public class Apartment:IEntity
    {
        public int Id { get; set; }
        public string? WhichBlock { get; set; }
        public bool? ApartmentState { get; set; }
        public int ApartmentNo { get; set; }
        public int? FloorNumber { get; set; }
        public string? ApartmentSize { get; set; }
        public bool? Status { get; set; }
        public int? DepartmentNumber { get; set; }

    }
}
