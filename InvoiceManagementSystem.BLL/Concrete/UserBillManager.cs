using InvoiceManagementSystem.BLL.Abstract;
using InvoiceManagementSystem.BLL.Constants;
using InvoiceManagementSystem.Core.Entities.Concrete;
using InvoiceManagementSystem.Core.Result;
using InvoiceManagementSystem.DAL.Abstract;
using InvoiceManagementSystem.Entity.Concrete;
using InvoiceManagementSystem.Entity.Dtos.BillDtos;
using InvoiceManagementSystem.Entity.Dtos.UserBillDtos;
using InvoiceManagementSystem.Entity.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.BLL.Concrete
{
    public class UserBillManager : IUserBillService
    {
        private readonly IUserBillDal _userBillDal;
        private readonly IUserService _userService;
        private readonly IBillService _billService;
        private readonly IBillTypesDal _billTypesDal;
        private readonly ISessionService _sessionService;
        private readonly IUserDal _usersDal;
        public UserBillManager(IUserBillDal userBill, IUserService userService, IBillService billService, IBillTypesDal billTypesDal, ISessionService sessionService, IUserDal usersDal)
        {
            _userBillDal = userBill;
            _userService = userService;
            _billService = billService;
            _billTypesDal = billTypesDal;
            _sessionService = sessionService;
            _usersDal = usersDal;
        }

        public IDataResult<UserBillAddDto> Add(UserBillAddDto userBillAdd)
        {
            try
            {
                var usersBills = new UserBill();
                if (userBillAdd == null)
                {
                    return new ErrorDataResult<UserBillAddDto>(null, "Fatura-Kullanıcı atama işlemi için alanları doldurunuz !", Messages.err_null);
                }
                usersBills.BillId = userBillAdd.BillId;
                usersBills.UserId = userBillAdd.UserId;
                usersBills.Status = true;
                return new SuccessDataResult<UserBillAddDto>(userBillAdd, "Ekleme işlemi başarılı", Messages.success);

            }
            catch (Exception e)
            {
                return new ErrorDataResult<UserBillAddDto>(null, e.Message, Messages.unknown_err);
            }
        }


        public IDataResult<UserBillMultipleAddDto>AddMultiple(UserBillMultipleAddDto userBillMultipleAdd)
        {
            if (userBillMultipleAdd==null)
            {
                return new ErrorDataResult<UserBillMultipleAddDto>(null, "Alanlar boş geçilemez", Messages.err_null);
            }
            var userBill = new List<UserBill>();
            //var userIds = userBillMultipleAdd.UserBillsAddDtos.Select(x => x.UserId).ToArray();  //MANTIKSIZCA BİR ŞEY YAPTIM ÇÜNKÜ ZATEN BİR KULLANICININ BİRDEN FAZLA SU FATURASI OLABİLİR.
            //var userIdControl = _userBillDal.GetList(x => userBillMultipleAdd.UserBillsAddDtos.Select(y => y.UserId).Contains(x.UserId));
            foreach (var item in userBillMultipleAdd.UserBillsAddDtos)
            {
                //if (userIdControl.Any(x => x.UserId == item.UserId))
                //{
                //    return new ErrorDataResult<UserBillMultipleAddDto>(null, "Belirtilen kullanıcıya ait fatura zaten var", Messages.user_already_has_an_invoice);
                //}
                _userBillDal.Add(new UserBill
                {
                    BillId= item.BillId,
                    UserId=item.UserId,
                    Status=true
                });
            }
            var count = userBillMultipleAdd.UserBillsAddDtos.Count();
            return new SuccessDataResult<UserBillMultipleAddDto>(userBillMultipleAdd, $"{count} tane kayıt başarıyla eklendi", Messages.success);
        }

        public IDataResult<List<UserBillListDto>> BillsPayable(string token)
        {
            try
            {
                var tokenCheck = _sessionService.TokenCheck(token);
                if (tokenCheck==null)
                {
                    return new ErrorDataResult<List<UserBillListDto>>(null, "token not found", Messages.token_not_found);
                }

                var userId = tokenCheck.Data.UserId;
                var matchUser = _usersDal.Get(x => x.Id == userId).Id;

                var userBill = _userBillDal.Get(x => x.UserId == userId);
                if (userBill != null && userBill.Status==false)
                {
                    var userBillDto = new List<UserBillListDto>();
                    var userBills = _userBillDal.GetList().Where(x=>x.UserId==matchUser);

                     
                    foreach (var item in userBills)
                    {
                        var billName = _billTypesDal.Get(x => x.Id == item.BillId).BillName;
                        var userDetails = _usersDal.Get(x => x.Id == item.UserId);
                        userBillDto.Add(new UserBillListDto
                        {
                            Id = item.BillId,
                            BillName = billName,
                            Status = item.Status,
                            UserDetails = userDetails.Name + " " + userDetails.Surname
                        }); ;
                    }
                    return new SuccessDataResult<List<UserBillListDto>>(userBillDto);
                }
                return new ErrorDataResult<List<UserBillListDto>>(null, "Bu kişiye ait ödenecek fatura bulunamadı", Messages.data_not_found);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public IDataResult<UserBill> Delete(int id)
        {
            try
            {
                var result = _userBillDal.Get(x => x.Id == id);
                if (result != null)
                {
                    _userBillDal.Delete(result);
                    return new SuccessDataResult<UserBill>(result, "Kayıt başarıyla silindi", Messages.success);
                }
                return new ErrorDataResult<UserBill>(null, "Böyle bir kayıt bulunamadı", Messages.data_not_found);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<UserBill>(null, e.Message, Messages.unknown_err);

            }
        }

        public IDataResult<UserBill> DeletebyPassive(int id)
        {
            try
            {
                var result = _userBillDal.Get(x => x.Id == id);
                if (result != null)
                {
                    result.Status = false;
                    _userBillDal.Update(result);
                    return new SuccessDataResult<UserBill>(result, "Kayıt başarıyla silindi", Messages.success);
                }
                return new ErrorDataResult<UserBill>(null, "Kayıt bulunamadı", Messages.data_not_found);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<UserBill>(null, e.Message, Messages.unknown_err);

            }
        }

        public IDataResult<UserBill> Get(Expression<Func<UserBill, bool>> filter)
        {
            try
            {
                var userBill = _userBillDal.Get(filter);
                if (userBill==null)
                {
                    return new ErrorDataResult<UserBill>(null, "User's Bill Not Found", Messages.data_not_found);
                }
                return new SuccessDataResult<UserBill>(userBill, "Ok", Messages.success);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IDataResult<List<UserBillListDto>> GetActiveBills() //ÖDENMEMİŞ FATURALAR
        {
            try
            {
                
                var usersBills = _userBillDal.GetList().Where(x=>x.Status==true);
                var userBillDto = new List<UserBillListDto>();

                foreach (var userBill in usersBills)
                {
                    var billName = _billTypesDal.Get(x => x.Id == userBill.BillId).BillName; 
                    var userName = _userService.GetById(userBill.UserId).Data; 

                    userBillDto.Add(new UserBillListDto
                    { 
                        Id = userBill.Id,
                        BillName = billName, 
                        Status = userBill.Status,
                        UserDetails = userName.Name +" "+ userName.Surname +"  "+ userName.PhoneNumber,
                    }); 

                }
                return new SuccessDataResult<List<UserBillListDto>>(userBillDto, "Ok", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<List<UserBillListDto>>(new List<UserBillListDto>(), e.Message, Messages.unknown_err);
            }
        }

        public IDataResult<UserBillListDto> GetById(int id)
        {
            try
            {
                var result = _userBillDal.Get(x => x.Id == id);
                var userid = result.UserId;
                var billid = result.BillId;
                
                var getUserDetails = _userService.GetById(userid).Data;
                var getBillName = _billTypesDal.Get(X => X.Id == billid).BillName;
                if (result == null)
                {
                    return new ErrorDataResult<UserBillListDto>(null, "Data is not found", Messages.data_not_found);
                }
                var userBillDto = new UserBillListDto()
                {
                    BillName = getBillName,
                    Status = result.Status,
                    Id = result.Id,
                    UserDetails =getUserDetails.Name + " " + getUserDetails.Surname + "  " + getUserDetails.PhoneNumber
                };
                return new SuccessDataResult<UserBillListDto>(userBillDto, "OK", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<UserBillListDto>(null, e.Message, Messages.unknown_err);
            }
        }

        public IDataResult<List<UserBillListDto>> GetList()
        {
            try
            {
                var userBills = _userBillDal.GetList();
                var userBillDto = new List<UserBillListDto>();

                foreach (var userBill in userBills)
                {
                    var getUserName=_userService.GetById(userBill.BillId).Data;
                    var getBillName = _billTypesDal.Get(x => x.Id == userBill.BillId).BillName;

                    userBillDto.Add(new UserBillListDto
                    {
                        Id = userBill.Id,
                        BillName = getBillName,
                        UserDetails =getUserName.Name +" "+ getUserName.Surname+ "   "+getUserName.PhoneNumber,
                        Status = userBill.Status
                    });;
                }
                return new SuccessDataResult<List<UserBillListDto>>(userBillDto, "OK", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<List<UserBillListDto>>(new List<UserBillListDto>(), e.Message, Messages.unknown_err);
            }
        }

        public IDataResult<List<UserBillListDto>> GetPassiveBills() //ÖDENMİŞ FATURALAR-
        {
            try
            {
                var userBills = _userBillDal.GetList().Where(x => x.Status == false);
                var userBillDto = new List<UserBillListDto>();

                foreach (var userBill in userBills)
                {
                    var billName = _billTypesDal.Get(x => x.Id == userBill.BillId).BillName;
                    var userDetails = _userService.GetById(userBill.UserId).Data;
                    userBillDto.Add(new UserBillListDto
                    {
                        Id = userBill.Id,
                        BillName = billName,
                        Status = userBill.Status,
                        UserDetails = userDetails.Name + " "+ userDetails.Surname + "  " + userDetails.PhoneNumber
                    });

                }
                return new SuccessDataResult<List<UserBillListDto>>(userBillDto, "OK", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<List<UserBillListDto>>(new List<UserBillListDto>(), e.Message, Messages.unknown_err);
            }
        }

        public IDataResult<UserBillUpdateDto> Update(UserBillUpdateDto userUpdateDto)
        {
            try
            {
                var result = _userBillDal.Get(x => x.Id == userUpdateDto.Id);
                if (result==null)
                {
                    return new ErrorDataResult<UserBillUpdateDto>(null, "Data is not found", Messages.data_not_found);
                }

                result.UserId=userUpdateDto.UserId;
                result.BillId = userUpdateDto.BillId;
                result.Id = userUpdateDto.Id;
                result.Status = userUpdateDto.Status;

                return new SuccessDataResult<UserBillUpdateDto>(userUpdateDto, "OK", Messages.success);
            }
            catch (Exception E)
            {

                return new ErrorDataResult<UserBillUpdateDto>(null, E.Message, Messages.unknown_err);

            }
        }
    }
}
