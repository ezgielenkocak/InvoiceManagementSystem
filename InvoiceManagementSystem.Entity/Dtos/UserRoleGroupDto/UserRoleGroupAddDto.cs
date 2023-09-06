using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.UserRoleGroupDto
{
    public class UserRoleGroupAddDto:IDto
    {
        public int UserId { get; set; }
        public int RoleGroupId { get; set; }
    }
}
