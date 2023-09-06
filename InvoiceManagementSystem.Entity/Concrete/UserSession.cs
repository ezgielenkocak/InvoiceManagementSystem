using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Concrete
{
    public class UserSession:IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TokenString { get; set; }
        public string UserAgent { get; set; }
        public string Ip { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
