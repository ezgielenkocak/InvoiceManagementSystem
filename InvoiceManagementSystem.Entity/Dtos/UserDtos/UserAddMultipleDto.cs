﻿using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.UserDtos
{
    public class UserAddMultipleDto:IDto
    {
        public UserAddDto[] UserAddMultipleDtos { get; set; }
    }
}
