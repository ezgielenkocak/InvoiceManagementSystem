using InvoiceManagementSystem.Core.Entities.Concrete;
using InvoiceManagementSystem.Core.Result;
using InvoiceManagementSystem.Core.Security;
using InvoiceManagementSystem.Entity.Dtos;
using InvoiceManagementSystem.Entity.Dtos.AuthDtos;
using InvoiceManagementSystem.Entity.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.BLL.Abstract
{
    public interface IAuthService
    {
        IDataResult<AccessToken> CreateToken(User user);
        IDataResult<bool> RegisterForAdmin(UserRegisterAdminstratorDto userRegisterAdminstratorDto);
        IDataResult<User> UserExists(UserRegisterAdminstratorDto userRegisterDto);

        IDataResult<bool> ChangeUserPassword(ChangePasswordWithDto changePasswordWithDto);
        IDataResult<SecuritiesResponseDto> PasswordReset(LoginDto loginDto);
        IDataResult<SecuritiesResponseDto> Login(UserLoginDto userLoginDto);
        IDataResult<bool> PasswordReset(PasswordResetDto passwordResetDto);

        IDataResult<bool> CheckCodes(SecuritiesResponseDto authSecurityDto);
        IDataResult<AccessToken> CheckSecuritiesCode(SecuritiesResponseDto authSecurityCode);
    }
}
