using InvoiceManagementSystem.Core.Result;
using InvoiceManagementSystem.Entity.Dtos.UserRoleGroupDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.BLL.Abstract
{
    public interface IUserRoleGroupService
    {
        public IDataResult<bool> AddForRegister(UserRoleGroupAddDto userRoleGroupAddDto);
    }
}
