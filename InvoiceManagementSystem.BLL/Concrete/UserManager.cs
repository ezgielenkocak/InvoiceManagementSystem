using InvoiceManagementSystem.BLL.Abstract;
using InvoiceManagementSystem.BLL.Constants;
using InvoiceManagementSystem.Core.Entities.Concrete;
using InvoiceManagementSystem.Core.Result;
using InvoiceManagementSystem.Core.Security;
using InvoiceManagementSystem.DAL.Abstract;
using InvoiceManagementSystem.DAL.Concrete.EntityFramework;
using InvoiceManagementSystem.Entity.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.BLL.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly ITokenHelper _tokenHelper;
        public UserManager(IUserDal userDal, ITokenHelper tokenHelper)
        {
            _userDal = userDal;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<UserAddMultipleDto> Add(UserAddMultipleDto userAddDto)
        {
            try
            {
                if (userAddDto==null)
                {
                    return new ErrorDataResult<UserAddMultipleDto>(null, "Data is null", Messages.err_null);
                }
                foreach (var item in userAddDto.UserAddMultipleDtos)
                {
                    _userDal.Add(new User
                    {
                        Name=item.Name,
                        Surname=item.Surname,
                        PhoneNumber=item.PhoneNumber,
                        Email=item.Email,
                        Status=true,
                        TcNo=item.TcNo,
                        RegistrationDate=DateTime.Now,
                    });
                }
                var count = userAddDto.UserAddMultipleDtos.Count();
                return new SuccessDataResult<UserAddMultipleDto>(userAddDto, $"{count} tane kayıt başarıyla eklendi", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<UserAddMultipleDto>(null, e.Message, Messages.unknown_err);
            }
        }

        public IDataResult<User> Delete(int id)
        {
            try
            {
                var result = _userDal.Get(x => x.Id == id);
                if (id==null)
                {
                    return new ErrorDataResult<User>(null, "id alanı zorunludur !", Messages.data_is_required);
                }
                if (result==null)
                {
                    return new ErrorDataResult<User>(null, "Böyle bir kullanıcı buluunamadı", Messages.data_not_found);
                }
                _userDal.Delete(result);
                return new SuccessDataResult<User>(result, "Kayıt başarıyla silindi", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<User>(null, e.Message, Messages.unknown_err);

            }
        }

        public IDataResult<User> Get(Expression<Func<User, bool>> filter)
        {
            try
            {
                var user = _userDal.Get(filter);
                if (user==null)
                {
                    return new ErrorDataResult<User>(null, "User not found", Messages.success);
                }
                return new SuccessDataResult<User>(user, "Ok", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<User>(null, e.Message, Messages.unknown_err);

            }
        }

        public IDataResult<UserListDto> GetById(int id)
        {
            try
            {
                var result = _userDal.Get(x => x.Id == id);
                if (id==null)
                {
                    return new ErrorDataResult<UserListDto>(null, "id is required", Messages.data_is_required);
                }
                if (result ==null)
                {
                    return new ErrorDataResult<UserListDto>(null, "data not found", Messages.data_not_found);
                }
                var userListDto=new UserListDto()
                {
                    Id = result.Id,
                    Email = result.Email,
                    Name=result.Name,
                    PhoneNumber=result.PhoneNumber,
                    Status=result.Status.Value,
                    Surname=result.Surname,
                    TcNo=result.TcNo
                };
                return new SuccessDataResult<UserListDto>(userListDto, "Ok", Messages.success);
            } 
            catch (Exception e)
            {

                return new ErrorDataResult<UserListDto>(null, e.Message, Messages.unknown_err);

            }
        }

        public IDataResult<List<UserListDto>> GetList()
        {
            try
            {
                var users = _userDal.GetList();
                var usersDto = new List<UserListDto>();
                foreach (var user in users)
                {
                    usersDto.Add(new UserListDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Name = user.Name,
                        PhoneNumber = user.PhoneNumber,
                        Status = user.Status.Value,
                        Surname = user.Surname,
                        TcNo = user.TcNo
                    });
                }
                return new SuccessDataResult<List<UserListDto>>(usersDto, "Ok", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<List<UserListDto>>(new List<UserListDto>(), e.Message, Messages.unknown_err);
            }
        }

        public IDataResult<User> GetUserByMail(string email)
        {
            try
            {
                var result = _userDal.Get(x => x.Email == email);
                if (result != null)
                {
                    return new SuccessDataResult<User>(result, "Ok", Messages.success);

                }
                return new ErrorDataResult<User>(null, "User not found", Messages.user_not_found);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<User>(null, e.Message, Messages.unknown_err);

            }
        }

        public IDataResult<User> GetUserByPhone(string phoneNumber)
        {
            try
            {
                var result = _userDal.Get(x => x.PhoneNumber.Trim().Replace(" ", "") == phoneNumber.Trim().Replace(" ", ""));
                if (result != null)
                {
                    return new SuccessDataResult<User>(result, "Ok", Messages.success);
                }
                return new ErrorDataResult<User>(null, "User not found", Messages.user_not_found);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<User>(null, e.Message, Messages.unknown_err);

            }
        }

        public IDataResult<User> MakePassiveUser(int id)
        {
            try
            {
                if (id != null)
                {
                    var result = _userDal.Get(x => x.Id == id);
                     if (result.Status == false)
                         return new ErrorDataResult<User>(null, "Kullanıcı zaten aktif değil !", Messages.update_operation_fail);
                    result.Status = false;
                  
                    _userDal.Update(result);

                    return new SuccessDataResult<User>(result, "Ok", Messages.success);
                }
                return new ErrorDataResult<User>(null, "Kullanıcı silinemedi", Messages.delete_operation_fail);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<User>(null, e.Message, Messages.unknown_err);
            }
        }

        public IDataResult<UserUpdateDto> Update(UserUpdateDto userUpdateDto)
        {
            try
            {
                var resullt = _userDal.Get(x => x.Id == userUpdateDto.Id);
                if (resullt==null)
                {
                    return new ErrorDataResult<UserUpdateDto>(null, "Güncelleme işlemi gerçekleştirilemedi", Messages.data_not_found);
                }
                if (userUpdateDto==null)
                {
                    return new ErrorDataResult<UserUpdateDto>(null, "Güncelleme  işlemi  gerçekleştirilemedi", Messages.data_not_found);
                }
                if (userUpdateDto.Name != null)
                {
                    resullt.Name = userUpdateDto.Name;
                }
                if (userUpdateDto.Id != null)
                {
                    resullt.Id=userUpdateDto.Id;
                }
              
                if (userUpdateDto.Email != null)
                {
                    resullt.Email = userUpdateDto.Email;
                }
                if (userUpdateDto.Status != null)
                {
                    resullt.Status = userUpdateDto.Status;
                }
                if (userUpdateDto.PhoneNumber != null)
                {
                    resullt.PhoneNumber = userUpdateDto.PhoneNumber;
                }
                if (userUpdateDto.Surname != null)
                {
                    resullt.Surname = userUpdateDto.Surname;
                }
                if (userUpdateDto.TcNo != null)
                {
                    resullt.TcNo = userUpdateDto.TcNo;
                }

                return new SuccessDataResult<UserUpdateDto>(userUpdateDto, "Güncelleme işlemi tamamlandı", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<UserUpdateDto>(null, e.Message, Messages.unknown_err);
            }
        }

        public IDataResult<bool> UpdateBasic(User user)
        {
            try
            {
                if (user != null)
                {
                    _userDal.Update(user);
                    return new SuccessDataResult<bool>(true, "Ok", Messages.success);
                }
                return new ErrorDataResult<bool>(true, null, Messages.update_operation_fail);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<bool>(false, e.Message, Messages.unknown_err);
            }
        }
    }
}
