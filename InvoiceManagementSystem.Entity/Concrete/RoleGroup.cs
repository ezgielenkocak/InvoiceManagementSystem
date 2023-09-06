using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Concrete
{
    public class RoleGroup:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public int CreatedById { get; set; }
        public int MoDifiedById { get; set; }
        public bool Status { get; set; }
        public bool IsLimitless { get; set; }

    }
}
