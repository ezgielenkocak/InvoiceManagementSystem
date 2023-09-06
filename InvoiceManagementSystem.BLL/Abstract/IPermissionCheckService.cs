using InvoiceManagementSystem.Core.Result;
using InvoiceManagementSystem.Entity.Dtos.PermissionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.BLL.Abstract
{
    public interface IPermissionCheckService
    {
        IDataResult<bool> CheckPermission(PermissionCheckDto permissionCheckDto);
    }
}
