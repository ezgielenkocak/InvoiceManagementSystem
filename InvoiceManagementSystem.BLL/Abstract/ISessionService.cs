using InvoiceManagementSystem.Core.Result;
using InvoiceManagementSystem.DAL.Abstract;
using InvoiceManagementSystem.Entity.Dtos.SessionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.BLL.Abstract
{
    public interface ISessionService
    {
        public IDataResult<SessionCheckResponseDto> TokenCheck(string token);
        public IDataResult<SessionCheckResponseWithUserDto> CheckAllControls(string token, string permission);
        //ProcData UserCheckDeniedProcess(int userId);
    }
}
