using InvoiceManagementSystem.Core.Entities;
using InvoiceManagementSystem.Entity.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.AuthDtos
{
    public class PasswordResetDto:IDto
    {
        public string SecurityCode { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordAgain { get; set; }
        public List<SecuritiesResponseDto> Securities { get; set; }

    }
}
