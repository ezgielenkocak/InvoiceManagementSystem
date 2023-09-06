using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.SessionDtos
{
    public class SessionCheckResponseDto:IDto
    {
        public bool Success { get; set; }
        public int UserId { get; set; }
        public string? Email { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
