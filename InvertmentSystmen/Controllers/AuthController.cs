using InvoiceManagementSystem.BLL.Abstract;
using InvoiceManagementSystem.Entity.Dtos;
using InvoiceManagementSystem.Entity.Dtos.PermissionDtos;
using InvoiceManagementSystem.Entity.Dtos.UserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ISessionService _sessionService; //test amaçlı kullanıyorum burada
        private readonly IPermissionCheckService _permissionCheckService;
        public AuthController(IAuthService authService, ISessionService sessionService, IPermissionCheckService permissionCheckService)
        {
            _authService = authService;
            _sessionService = sessionService;
            _permissionCheckService = permissionCheckService;
        }


        //TEST AMAÇLI APİ
        [HttpPost("checkallcontrols")]
        public IActionResult CheckAllControls(string token, string permission)
        {
            var result=_sessionService.CheckAllControls(token, permission);
            return Ok(result);
        }
        //TEST AMAÇLI APİ
        [HttpPost("TokenCheck")]
        public IActionResult CheckAllControls(string token)
        {
            var result = _sessionService.TokenCheck(token);
            return Ok(result);
        }
        //TEST AMAÇLI APİ
        [HttpPost("checkpermission")]
        public IActionResult CheckAllControls(PermissionCheckDto permissionCheckDto)
        {
            var result = _permissionCheckService.CheckPermission(permissionCheckDto);
            return Ok(result);
        }
        [HttpPost("login")]
        public IActionResult Login(UserLoginDto loginDto)
        {
            var result = _authService.Login(loginDto);
            return Ok(result);

        }

        [HttpPost("registerforadmin")]
        public IActionResult Register(UserRegisterAdminstratorDto registerAdminstratorDto)
        {
            var result = _authService.RegisterForAdmin(registerAdminstratorDto);

            return Ok(result);
        }

        [HttpPost("passwordreset")]
        public IActionResult PasswordRest(LoginDto loginDto)
        {
            var result = _authService.PasswordReset(loginDto);
            return Ok(result);
        }

        [HttpPost("changeuserpassword")]
        public IActionResult ChangeUserPassword(ChangePasswordWithDto dto)
        {
            var result = _authService.ChangeUserPassword(dto);
            return Ok(result);
        }

        [HttpPost("checksecuritiescode")]
        public IActionResult CheckSecuritiesCode(SecuritiesResponseDto authSecurityResponseDto)
        {
            var result = _authService.CheckSecuritiesCode(authSecurityResponseDto);
            return Ok(result);
        }

        [HttpPost("checkcodes")]
        public IActionResult CheckCodes(SecuritiesResponseDto authSecurityResponseDto)
        {
            var result = _authService.CheckCodes(authSecurityResponseDto);
            return Ok(result);
        }
    }

}
