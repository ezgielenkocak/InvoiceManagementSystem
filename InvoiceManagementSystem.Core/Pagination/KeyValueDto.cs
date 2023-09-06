﻿using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Core.Pagination
{
    public class KeyValueDto:IDto
    {
        public string Key { get; set; }
        public object Value { get; set; }
        public string Parameter { get; set; }
    }
}
