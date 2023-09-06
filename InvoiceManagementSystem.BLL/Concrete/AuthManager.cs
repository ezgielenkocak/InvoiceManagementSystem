using System.Globalization;
using InvoiceManagementSystem.BLL.Abstract;
using InvoiceManagementSystem.BLL.Constants;
using InvoiceManagementSystem.Core.Entities.Concrete;
using InvoiceManagementSystem.Core.Result;
using InvoiceManagementSystem.Core.Security;
using InvoiceManagementSystem.DAL.Abstract;
using InvoiceManagementSystem.DAL.Concrete.Context;
using InvoiceManagementSystem.Entity.Dtos;
using InvoiceManagementSystem.Entity.Dtos.AuthDtos;
using InvoiceManagementSystem.Entity.Dtos.UserDtos;
using Microsoft.AspNetCore.Http;
using PhoneNumbers;
using Snickler.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using InvoiceManagementSystem.Entity.Concrete;
using InvoiceManagementSystem.Entity.Enums;

namespace InvoiceManagementSystem.BLL.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserDal _userDal;
        private readonly IRoleGroupDal _roleGroupDal;
        private readonly IUserRoleGroupService _userRoleGroupService;
        private readonly ISessionService _sessionService;


        private IHttpContextAccessor _httpContextAccessor;
        private readonly IUserSecurityHistoriesDal _userSecurityHistoriesDal;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IUserDal userDal, IHttpContextAccessor httpContextAccessor, IRoleGroupDal groupDal, IUserSecurityHistoriesDal userSecurityHistoriesDal, IUserRoleGroupService userRoleGroupService, ISessionService sessionService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userDal = userDal;
            _httpContextAccessor = httpContextAccessor;
            _roleGroupDal = groupDal;
            _userSecurityHistoriesDal = userSecurityHistoriesDal;
            _userRoleGroupService = userRoleGroupService;
            _sessionService = sessionService;
        }

        public IDataResult<bool> ChangeUserPassword(ChangePasswordWithDto changePasswordWithDto)
        {
            try
            {
                var tokenCheck = _sessionService.TokenCheck(changePasswordWithDto.Token);
                if (tokenCheck == null)
                {
                    return new ErrorDataResult<bool>(false, tokenCheck.Message, Messages.token_expired);
                }
                var user = _userDal.Get(x => x.SecurityCode.Trim().Replace(" ", "") == changePasswordWithDto.SecurityCode.Trim().Replace(" ", ""));
                if (user == null)
                {
                    return new ErrorDataResult<bool>(false, "Kullanıcı bulunamadı", Messages.user_not_found);
                }
                if (!HashingHelper.VerifyPasswordHash(changePasswordWithDto.CurrentPassword, user.PasswordSalt, user.PasswordHash))
                {
                    return new ErrorDataResult<bool>(false, "Şu anki şifre yanlış girildi", Messages.current_password_is_wrong);
                }
                if (changePasswordWithDto.NewPassword != changePasswordWithDto.NewPasswordClone)
                {
                    return new ErrorDataResult<bool>(false, "Şifreler uyuşmuyor", Messages.password_are_not_match);
                }
                if (user.SecurityCode != changePasswordWithDto.SecurityCode)
                {
                    return new ErrorDataResult<bool>(false, "Yanlış Security Kod", Messages.user_wrong_code);
                }
                SecuritiesResponseDto dto = new()
                {
                    SecurityCode = user.SecurityCode,
                };

                byte[] passwordsalt, passwordhash;
                HashingHelper.CreatePasswordHash(changePasswordWithDto.NewPassword, out passwordsalt, out passwordhash);
                user.PasswordSalt = passwordsalt;
                user.PasswordHash = passwordhash;
                
                _userDal.Update(user);
                return new SuccessDataResult<bool>(true, "Yeni şifre oluşturuldu !", Messages.success);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public IDataResult<AccessToken> CreateToken(User user)
        {
            try
            {
                var token = _tokenHelper.CreateNewToken(user);
                var accessToken = new AccessToken();
                using (var context = new ImsDbContext())
                {
                    context.LoadStoredProc("dbo.proc_CreateToken")
                        .WithSqlParam("userId", token.UserId)
                        .WithSqlParam("tokenString", token.TokenString)
                        .WithSqlParam("userAgent", token.UserAgent)
                        .WithSqlParam("ip", token.Ip)
                        .ExecuteStoredProc((handler) =>
                        {
                            var data = handler.ReadToList<AccessToken>().FirstOrDefault();
                            accessToken = data;
                        });
                }
                if (accessToken == null)
                {
                    return new ErrorDataResult<AccessToken>(new AccessToken(), "Generate Token Failed", Messages.generate_token_failed);
                }
                return new SuccessDataResult<AccessToken>(accessToken, "token created", Messages.success);

            }
            catch (Exception e)
            {

                return new ErrorDataResult<AccessToken>(null, e.Message, Messages.unknown_err);

            }
        }

        public IDataResult<SecuritiesResponseDto> Login(UserLoginDto userLoginDto)
        {

            var userToCheck = _userDal.Get(x => x.Email == userLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<SecuritiesResponseDto>(null, "user not found", Messages.user_not_found);
            }
            if (userToCheck != null)
            {
                if (userToCheck.Status == false)
                {
                    return new ErrorDataResult<SecuritiesResponseDto>(null, "user is not active", Messages.user_not_active);
                }
            }


            if (!HashingHelper.VerifyPasswordHash(userLoginDto.Password, userToCheck.PasswordSalt, userToCheck.PasswordHash))
            {
                return new ErrorDataResult<SecuritiesResponseDto>(null, "password is wrong", Messages.user_wrong_password);
            }

            userToCheck.SecurityCode = RandomString(15);
            _userService.UpdateBasic(userToCheck);
             var userSecurityHistory=new UserSecurityHistory(){
                 UserId=userToCheck.Id,
                 CreatedDate=DateTime.Now,
                 SecurityCode=userToCheck.SecurityCode,
                 ExpireDate=DateTime.Now.AddDays(1),

             };
            _userSecurityHistoriesDal.Add(userSecurityHistory);
            var getUserSecurityTypes = GetUserSecurityTypes(userToCheck.Id);
            return new SuccessDataResult<SecuritiesResponseDto>(getUserSecurityTypes.Data, "user security types:", Messages.success); /////çalışmayadabilir
        }

        public static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length) //belirtilen bir nesneyi belirtilen sayıda kez içeren bir IEnumerable koleksiyonu oluşturur. Yani, bu metodu kullanarak bir karakter dizisini veya başka bir nesneyi belirtilen sayıda tekrarlayarak yeni bir koleksiyon oluşturabilirsin

              .Select(s => s[random.Next(s.Length)]).ToArray()); // koleksiyondaki her bir karakteri, koleksiyonun uzunluğu içinde rasgele bir karakter seçerek değiştirir. //ToArray() ifadesi, koleksiyonu karakter dizisine dönüştürür.
        }

        private IDataResult<SecuritiesResponseDto> GetUserSecurityTypes(int userId)
        {
            try
            {
                SecuritiesResponseDto mSec = new();
                List<SecuritySystem> mResponse = new();

                var getuser = _userDal.Get(x => x.Id == userId);

                if (getuser.Status == false)
                {
                    return new ErrorDataResult<SecuritiesResponseDto>(null, "user is not active", Messages.unknown_err);
                }


                SecuritySystem ss = new()
                {
                    SecurityCode = null,
                    SecurityType = "email",
                    Status = getuser.Status.Value
                };
                mResponse.Add(ss);


                mSec.SecuritySystems = mResponse;
                mSec.SecurityCode = getuser.SecurityCode;

                return new SuccessDataResult<SecuritiesResponseDto>(mSec, "ok", Messages.success);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<SecuritiesResponseDto>(null, e.Message, Messages.unknown_err);
            }
        }

        public IDataResult<bool> RegisterForAdmin(UserRegisterAdminstratorDto userRegisterAdminstratorDto)
        {
            //try
            //{
                var tokenCheck = _sessionService.CheckAllControls(userRegisterAdminstratorDto.Token, Permission.per_adduser);
       
                if(!tokenCheck.Success)
                {
                    return new ErrorDataResult<bool>(false, tokenCheck.MessageCode, tokenCheck.Message);
                }
                if (!userRegisterAdminstratorDto.Email.Contains("@"))
                {
                    return new ErrorDataResult<bool>(false, "Lütfen geçerli bir e-posta giriniz", Messages.invalid_email);
                }
                PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance(); //Bu class singletondur.Yani tek bir örnek oluşturmayı sağlar o yüzden getinstance kullanılmalı
                PhoneNumber phoneNumber = phoneUtil.Parse(userRegisterAdminstratorDto.PhoneNumber, "TR");

                if (!phoneUtil.IsValidNumber(phoneNumber))
                {
                    return new ErrorDataResult<bool>(false, "Invalid PhoneNumber", Messages.invalid_phone_number);
                }
                byte[] passworsalt, passwordhash;
                var createPasswordBySystem = SendEmailDto.RandomString(6);
                HashingHelper.CreatePasswordHash(createPasswordBySystem, out passworsalt, out passwordhash);

                var NameToTitleCase = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(userRegisterAdminstratorDto.Name.ToLower().Trim().Replace(" ", "").Replace("-", " "));
                var SurnameToTitleCase = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(userRegisterAdminstratorDto.SurName.ToLower().Replace(" ", "").Replace("-", " "));

                var user = new User
                {
                    Name = NameToTitleCase,
                    Surname = SurnameToTitleCase.Trim(),
                    Email = userRegisterAdminstratorDto.Email.Trim().ToLower(),
                    RegistrationDate = DateTime.Now,
                    Status = true,
                    TcNo = userRegisterAdminstratorDto.IdentityNo,
                    SecurityCode = SendEmailDto.RandomString(6),
                    PhoneNumber = userRegisterAdminstratorDto.PhoneNumber.Trim(),
                    PasswordHash = passwordhash,
                    PasswordSalt = passworsalt
                };
                _userDal.Add(user);


                SendEmailDto sendEmailDto = new();
                string replPhoneNumber = "";
                if (user.PhoneNumber.StartsWith("+90"))
                {
                    replPhoneNumber = user.PhoneNumber.Replace("+90", "").Trim().Replace(" ", "");
                }
                sendEmailDto.SendEmail(userRegisterAdminstratorDto.Email, user.Name + " " + user.Surname, createPasswordBySystem);

                SecuritiesResponseDto securitiesResponseDto = GetUserSecurityTypes(user.Id).Data;

                var getRoleGroup = _roleGroupDal.Get(x => x.Name == "Member");
                var addDepartment = _userRoleGroupService.AddForRegister(new Entity.Dtos.UserRoleGroupDto.UserRoleGroupAddDto { RoleGroupId = getRoleGroup.Id, UserId = user.Id });
                return new SuccessDataResult<bool>(true, "Kayıt başarıyla oluşturuldu", Messages.success);
            //}
            //catch (Exception e)
            //{

            //    return new ErrorDataResult<bool>(false, e.Message, Messages.unknown_err);
            //}
        }

        public IDataResult<User> UserExists(UserRegisterAdminstratorDto userRegisterDto)
        {
            try
            {

                var checkEmail = _userService.GetUserByMail(userRegisterDto.Email);
                if (checkEmail.Success)
                {
                    return new ErrorDataResult<User>(null, "Bu maille daha önce kayıt oluşturulmuş", Messages.already_mail_registered);
                }
                var checkPhone = _userService.GetUserByPhone(userRegisterDto.PhoneNumber);
                if (checkPhone.Success)
                {
                    return new ErrorDataResult<User>(null, "Bu telefon numarasıyla daha önce kayıt oluşturulmuş", Messages.already_phone_number_exists);
                }
                return new SuccessDataResult<User>(null, "Kayıt başarıyla oluşturuldu", Messages.success);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<User>(null, e.Message, Messages.unknown_err);

            }
        }

        public IDataResult<SecuritiesResponseDto> PasswordReset(LoginDto loginDto)
        {
            try
            {
                SecuritiesResponseDto securitiesResponseDto = new();
                var userToCheck = _userService.GetUserByMail(loginDto.Email);
                if (userToCheck.Data == null)
                {
                    return new ErrorDataResult<SecuritiesResponseDto>(null, "User not found", Messages.user_not_found);
                }
                if (userToCheck.Data.Status == false)
                {
                    return new ErrorDataResult<SecuritiesResponseDto>(null, "User not active", Messages.user_not_active);
                }
                userToCheck.Data.SecurityCode = RandomString(4);
                _userDal.Update(userToCheck.Data);

                var getUser = _userDal.Get(u => u.Id == userToCheck.Data.Id);
                securitiesResponseDto.SecurityCode = getUser.SecurityCode;
                return new SuccessDataResult<SecuritiesResponseDto>(securitiesResponseDto, "Ok", Messages.success);
            }

            catch (Exception e)
            {

                return new ErrorDataResult<SecuritiesResponseDto>(null, e.Message, Messages.unknown_err);
            }
        }

        public IDataResult<bool> PasswordReset(PasswordResetDto passwordResetDto)
        {
            try
            {
                SecuritiesResponseDto securitiesResponseDto = new SecuritiesResponseDto();
                var user = _userService.Get(x => x.SecurityCode.Trim().Replace(" ", "") == passwordResetDto.SecurityCode.Trim().Replace(" ", "")).Data;
                if (user == null)
                {
                    return new ErrorDataResult<bool>(false, "User not found", Messages.user_not_found);
                }
                if (user.Status == false)
                {
                    return new ErrorDataResult<bool>(false, "User not active", Messages.user_not_active);
                }
                if (user.Email != passwordResetDto.Email)
                {
                    return new ErrorDataResult<bool>(false, "Mailler eşleşmiyor", Messages.mails_not_matching);
                }
                var getName = user.Name;
                string[] passwordValidation = getName.Split(' ');
                foreach (var item in passwordValidation)
                {
                    if (passwordResetDto.NewPassword.Replace(" ", "").ToLower().Contains(item.Replace(" ", "").ToLower()) && item != "")
                    {
                        return new ErrorDataResult<bool>(false, "Parola isim içeremez", Messages.password_cant_contain_name);
                    }
                    if (passwordResetDto.NewPassword.Replace(" ", "").ToLower().Contains(user.Surname))
                    {
                        return new ErrorDataResult<bool>(false, "Parola soyisim içeremez", Messages.password_cant_contain_surname);
                    }
                    if (user.SecurityCode != passwordResetDto.SecurityCode)
                    {
                        return new ErrorDataResult<bool>(false, "Security Code yanlış", Messages.user_wrong_code);
                    }
                    if (passwordResetDto.NewPassword != passwordResetDto.NewPasswordAgain)
                    {
                        return new ErrorDataResult<bool>(false, "Passwords are not matcihg", Messages.password_are_not_match);
                    }
                    user.SecurityCode = RandomString(10);
                    _userService.UpdateBasic(user);
                    securitiesResponseDto.SecurityCode = user.SecurityCode;
                    securitiesResponseDto.Email = user.Email;
                }
                return new SuccessDataResult<bool>(true, "Ok", Messages.success);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public IDataResult<AccessToken> CheckSecuritiesCode(SecuritiesResponseDto authSecurityCode)
        {
            try
            {
                if (authSecurityCode.SecuritySystems == null || authSecurityCode.SecurityCode == null)
                {
                    return new ErrorDataResult<AccessToken>(null, "Kullanıcı Kontrol Kodu Boş", Messages.user_control_code_is_null);
                }

                var checkUser = _userDal.Get(x => x.Email == authSecurityCode.Email);
                if (checkUser == null || checkUser.SecurityCode != authSecurityCode.SecurityCode)
                {
                    return new ErrorDataResult<AccessToken>(null, "Kontrol Kodu yanlış girildi", Messages.user_control_code_is_wrong);
                }

                var checkCodes = CheckCodes(authSecurityCode);
                if (!checkCodes.Data)
                {
                    return new ErrorDataResult<AccessToken>(null, checkCodes.Message, checkCodes.MessageCode);
                }

                var result = CreateToken(checkUser);
           
                if (!result.Success)
                {
                    return new ErrorDataResult<AccessToken>(null, "Token Üretilirken Hata Oluştu", Messages.generate_token_failed);
                }
                checkUser.SecurityCode = authSecurityCode.SecurityCode;
                _userService.UpdateBasic(checkUser);

                return new SuccessDataResult<AccessToken>(result.Data, "Ok", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<AccessToken>(null, e.Message, Messages.success);

            }
        }

        public IDataResult<bool> CheckCodes(SecuritiesResponseDto authSecurityDto)
        {
            try
            {
                var checkUser = _userDal.Get(x => x.Email.Trim().ToLower() == authSecurityDto.Email.Trim().ToLower());
                if (authSecurityDto.SecuritySystems.Count > 0)
                {
                    foreach (var item in authSecurityDto.SecuritySystems)
                    {
                        if (item.SecurityCode == null || item.SecurityCode == "")
                        {
                            return new ErrorDataResult<bool>(false, "Güvenlik Kodu Boş Geçilemez", Messages.user_control_code_is_wrong);
                        }

                        var securityHistory = _userSecurityHistoriesDal.GetList(x => x.UserId == checkUser.Id).OrderBy(x => x.Id).LastOrDefault();
                        if (DateTime.Now > securityHistory.ExpireDate)
                        {
                            return new ErrorDataResult<bool>(false, "Kontrol Kodunun süresi doldu. Lütfen yeni bir tane alınız", Messages.err_code_expired);
                        }
                    }
                    return new SuccessDataResult<bool>(true);
                }
                return new ErrorDataResult<bool>(false, "Kayıt bulunamadı", Messages.data_not_found);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<bool>(false, e.Message, Messages.unknown_err);

            }
        }
    }
}
