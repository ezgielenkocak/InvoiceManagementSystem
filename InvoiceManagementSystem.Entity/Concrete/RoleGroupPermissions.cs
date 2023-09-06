using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Concrete
{
    public class RoleGroupPermissions:IEntity
    {
        public int Id { get; set; }
        public int RoleGroupId { get; set; }
        public int PermissionId { get; set; }
    }
}
