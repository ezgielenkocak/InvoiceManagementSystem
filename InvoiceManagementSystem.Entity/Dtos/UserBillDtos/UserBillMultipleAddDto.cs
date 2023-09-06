using InvoiceManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos.UserBillDtos
{
    public class UserBillMultipleAddDto:IEntity
    {
        public UserBillAddDto[] UserBillsAddDtos { get; set; }
    }
}
