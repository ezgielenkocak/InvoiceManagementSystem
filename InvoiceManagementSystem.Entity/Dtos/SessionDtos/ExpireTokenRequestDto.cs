using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.SessionDtos
{
    public class ExpireTokenRequestDto:IDto
    {
        public string RequestToken { get; set; }
    }
}
