using InvoiceManagementSystem.BLL.Abstract;
using InvoiceManagementSystem.BLL.Constants;
using InvoiceManagementSystem.Core.Result;
using InvoiceManagementSystem.DAL.Abstract;
using InvoiceManagementSystem.DAL.Concrete.Context;
using InvoiceManagementSystem.Entity.Dtos.PermissionDtos;
using InvoiceManagementSystem.Entity.Dtos.SessionDtos;
using Snickler.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.BLL.Concrete
{
    public class SessionManager : ISessionService
    {
        private readonly IUserDal _usersDal;
        private readonly IPermissionCheckService _permissionCheckService;
        public SessionManager(IUserDal usersDal, IPermissionCheckService permissionCheckService)
        {
            _usersDal = usersDal;
            _permissionCheckService = permissionCheckService;
        }

        public IDataResult<SessionCheckResponseWithUserDto> CheckAllControls(string token, string permission)
        {
            try
            {
                var tokenCheckResult = TokenCheck(token);
                if (!tokenCheckResult.Data.Success)
                {
                    return new ErrorDataResult<SessionCheckResponseWithUserDto>(new SessionCheckResponseWithUserDto(), tokenCheckResult.Message, tokenCheckResult.MessageCode);
                }

                var user = _usersDal.Get(x => x.Email == tokenCheckResult.Data.Email);
                if (user == null)
                {
                    return new ErrorDataResult<SessionCheckResponseWithUserDto>(new SessionCheckResponseWithUserDto(), "User not found", Messages.user_not_found);
                }

                var session = new SessionCheckResponseWithUserDto
                {
                    Success = true,
                    ExpireDate = tokenCheckResult.Data.ExpireDate.Value,
                    User = user
                };

                PermissionCheckDto permissionCheckDto = new()
                {
                    Token = token,
                    Permission = permission,
                    UserId = tokenCheckResult.Data.UserId
                };

                var permissionCheckResult = _permissionCheckService.CheckPermission(permissionCheckDto);
                if (!permissionCheckResult.Data)
                {
                    return new ErrorDataResult<SessionCheckResponseWithUserDto>(new SessionCheckResponseWithUserDto(), permissionCheckResult.Message, permissionCheckResult.MessageCode);
                }
                return new SuccessDataResult<SessionCheckResponseWithUserDto>(session, "Ok", Messages.success);

            }
            catch (Exception e)
            {
                return new SuccessDataResult<SessionCheckResponseWithUserDto>(new SessionCheckResponseWithUserDto(), e.Message, Messages.unknown_err);

            }
        }



        public IDataResult<SessionCheckResponseDto> TokenCheck(string token)
        {
            try
            {
                var result = new SessionCheckResponseDto();
                var sessionCheckList = new UsersSessionCheckDto();

                using (var context = new ImsDbContext())
                {
                    context.LoadStoredProc("dbo.proc_CheckUserSession")
                        .WithSqlParam("token", token)
                        .ExecuteStoredProc((handler) =>
                        {
                            var list = handler.ReadToList<UsersSessionCheckDto>().FirstOrDefault();
                            sessionCheckList = list;
                        });
                }
                if (sessionCheckList == null)
                {
                    return new ErrorDataResult<SessionCheckResponseDto>(new SessionCheckResponseDto(), "Token Not Found", Messages.token_not_found);
                }
                result.Success = sessionCheckList.EndDate > DateTime.Now;
                result.UserId = sessionCheckList.UserId;
                result.ExpireDate = sessionCheckList.EndDate;
                result.Email = sessionCheckList.Email;
                if (!result.Success)
                {
                    return new ErrorDataResult<SessionCheckResponseDto>(result, "Token Expired", Messages.token_expired);
                }
                return new SuccessDataResult<SessionCheckResponseDto>(result);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<SessionCheckResponseDto>(new SessionCheckResponseDto(), e.Message, Messages.unknown_err);
            }
        }
    }
}
