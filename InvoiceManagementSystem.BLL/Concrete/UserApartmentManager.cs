using InvoiceManagementSystem.BLL.Abstract;
using InvoiceManagementSystem.BLL.Constants;
using InvoiceManagementSystem.Core.Result;
using InvoiceManagementSystem.DAL.Abstract;
using InvoiceManagementSystem.Entity.Concrete;
using InvoiceManagementSystem.Entity.Dtos.UserApartmentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.BLL.Concrete
{
    public class UserApartmentManager : IUserApartmentService
    {
        private readonly IUserApartmentDal _userApartmentDal;
        private readonly IUserService _userService;
        private readonly IUserDal _userDal;
        private readonly IApartmentService _apartmentService;
        private readonly IApartmentDal _apartmentDal;
        public UserApartmentManager(IUserApartmentDal userApartmentDal, IUserService userService, IApartmentService apartmentService, IUserDal userDal, IApartmentDal apartmentDal)
        {
            _userApartmentDal = userApartmentDal;
            _userService = userService;
            _apartmentService = apartmentService;
            _userDal = userDal;
            _apartmentDal = apartmentDal;
        }

        public IDataResult<List<UserApartmentListDto>> ActiveUserGetList()
        {
            try
            {
                var userApartments = _userApartmentDal.GetList();
                var userApartmentDto = new List<UserApartmentListDto>();
                foreach (var userApartment in userApartments)
                {
                    var userDetails = _userService.Get(x => x.Id == userApartment.UserId);
                    var apartmentDetails = _apartmentService.Get(x => x.Id == userApartment.ApartmentId);
                    var apartmentNo = _apartmentService.GetById(userApartment.Id);
                    userApartmentDto.Add(new UserApartmentListDto
                    {
                        UserDetails=userDetails.Data.Name +" "+ userDetails.Data.Surname,
                        ApartmentNo = apartmentDetails.Data.ApartmentNo,
                        BlockName = apartmentDetails.Data.WhichBlock,
                        FloorNumber= apartmentDetails.Data.FloorNumber,
                       
                    }); ;
                }
                return new SuccessDataResult<List<UserApartmentListDto>>(userApartmentDto, "Ok", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<List<UserApartmentListDto>>(new List<UserApartmentListDto>(), e.Message, Messages.unknown_err);

          }
        }

        public IDataResult<bool> Add(UserApartmentAddDto userApartmentAddDto)
        {
            try
            {
                var apartment = _apartmentDal.Get(x => x.Id == userApartmentAddDto.ApartmentId);
                var userApartments = new UserApartment();
                if (userApartmentAddDto==null)
                {
                    return new ErrorDataResult<bool>(false, "Kayıt eklemek için alanları doldurunuz", Messages.err_null);
                }
                userApartments.ApartmentId = userApartmentAddDto.ApartmentId;
                userApartments.UserId = userApartmentAddDto.UserId;


                _userApartmentDal.Add(userApartments);
                apartment.Status = false;

                return new SuccessDataResult<bool>(true, "Ok", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<bool>(false, e.Message, Messages.unknown_err);
            }

        }

        public IDataResult<UserApartmentAddMultipleDto> AddMultiple(UserApartmentAddMultipleDto dto)
        {
            try
            {
                if (dto==null)
                {
                    return new ErrorDataResult<UserApartmentAddMultipleDto>(null, "Ekleme işlemi yapabilmek için verileri giriniz", Messages.err_null);
                }
                var userApartments = new List<UserApartment>();
                foreach (var userApartment in dto.UserApartmentAddDtos)
                {
                    _userApartmentDal.Add(new UserApartment
                    {
                        UserId = userApartment.UserId,
                        ApartmentId=userApartment.ApartmentId,                      
                    });
                    var apartment=_apartmentDal.Get(x=>x.Id==userApartment.ApartmentId);
                    apartment.ApartmentState = false;
                }

                var count=dto.UserApartmentAddDtos.Count();
                return new SuccessDataResult<UserApartmentAddMultipleDto>(dto, $"{count} tane kayıt başarıyla eklendi", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<UserApartmentAddMultipleDto>(new UserApartmentAddMultipleDto(), e.Message, Messages.unknown_err);
            }
        }

        public IDataResult<bool> Delete(int id)
        {
            try
            {
                var result = _userApartmentDal.Get(x => x.Id == id);
                if (result==null || id==null)
                {
                    return new ErrorDataResult<bool>(false, "fail", Messages.err_null);
                }
                _userApartmentDal.Delete(result);
                return new SuccessDataResult<bool>(true, "Kayıt silindi", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<bool>(false, e.Message, Messages.unknown_err);
            }
        }

        public IDataResult<UserApartmentListDto> GetById(int id)
        {
            try
            {
                var result = _userApartmentDal.Get(x => x.Id == id);
              
                if (result==null ||  id==null)
                {
                    return new ErrorDataResult<UserApartmentListDto>(null, "user or apartment not found", Messages.err_null);
                }
                var getApartmentDetails = _apartmentService.Get(x => x.Id == result.ApartmentId).Data;
                var getUserDetails = _userDal.Get(x => x.Id == result.UserId);
                var dto=new UserApartmentListDto
                {
                    FloorNumber=getApartmentDetails.FloorNumber,
                    UserDetails=getUserDetails.Name + " "+ getUserDetails.Surname + "  "+ getUserDetails.PhoneNumber,
                    ApartmentNo=getApartmentDetails.ApartmentNo,
                    BlockName=getApartmentDetails.WhichBlock
                    
                };
               
                return new SuccessDataResult<UserApartmentListDto>(dto, "Ok", Messages.success);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<UserApartmentListDto>(null, e.Message, Messages.success);
            }
        }

        public IDataResult<List<UserApartmentListDto>> GetList()
        {
            try
            {
                var userApartments = _userApartmentDal.GetList();
                var userApartmentsDto = new List<UserApartmentListDto>();

                foreach (var item in userApartments)
                {
                    var apartmentDetails = _apartmentService.Get(x => x.Id == item.ApartmentId).Data;
                    var userDetails = _userService.Get(x => x.Id == item.UserId).Data;
                    userApartmentsDto.Add(new UserApartmentListDto
                    {
                   
                       
                        BlockName=apartmentDetails.WhichBlock,
                        ApartmentNo=apartmentDetails.ApartmentNo,
                        FloorNumber=apartmentDetails.FloorNumber,
                        UserDetails=userDetails.Name + "  " + userDetails.Surname + "  " + userDetails.PhoneNumber
                    });
                }
                return new SuccessDataResult<List<UserApartmentListDto>>(userApartmentsDto, "Ok", Messages.success);

            }
            catch (Exception e)
            {
                return new ErrorDataResult<List<UserApartmentListDto>>(new List<UserApartmentListDto>(), "Ok", Messages.unknown_err);
            }
        }

        public IDataResult<bool> Update(UserApartmentUpdateDto userApartmentUpdateDto)
        {
            try
            {
                if (userApartmentUpdateDto==null)
                {
                    return new ErrorDataResult<bool>(false, "Güncelleme işlemi yapabilmek için güncellenecek alanları boş bırakmayınız", Messages.err_null);
                }

                var result = _userApartmentDal.Get(x => x.Id == userApartmentUpdateDto.Id);
                if (result == null)
                    return new ErrorDataResult<bool>(false, "güncelleme işlemi başarısız", Messages.err_null);
                result.Id = userApartmentUpdateDto.Id;
                result.ApartmentId = userApartmentUpdateDto.ApartmentId;
                result.UserId = userApartmentUpdateDto.UserId;

                _userApartmentDal.Update(result);
                return new SuccessDataResult<bool>(true, "Ok", Messages.success);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<bool>(false, e.Message, Messages.unknown_err);
            }
        }
    }
}
