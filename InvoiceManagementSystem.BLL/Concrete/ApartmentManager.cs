using InvoiceManagementSystem.BLL.Abstract;
using InvoiceManagementSystem.BLL.Constants;
using InvoiceManagementSystem.Core.Entities.Concrete;
using InvoiceManagementSystem.Core.Result;
using InvoiceManagementSystem.DAL.Abstract;
using InvoiceManagementSystem.Entity.Concrete;
using InvoiceManagementSystem.Entity.Dtos;
using InvoiceManagementSystem.Entity.Dtos.ApartmentDtos;
using InvoiceManagementSystem.Entity.Dtos.UserDtos;
using InvoiceManagementSystem.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.BLL.Concrete
{
    public class ApartmentManager : IApartmentService
    {
        private readonly IApartmentDal _apartmentDal;
        private readonly ISessionService _sessionService;

        public ApartmentManager(IApartmentDal apartmentDal, ISessionService sessionService)
        {
            _apartmentDal = apartmentDal;
            _sessionService = sessionService;
        }

        public IDataResult<object> Add(AddMultipleApartmentDto addMultipleApartmentDto)
        {
            try
            {
                var tokenCheck = _sessionService.CheckAllControls(addMultipleApartmentDto.Token, Permission.per_addapartment);
                if (tokenCheck == null)
                {
                    return new ErrorDataResult<object>(null, tokenCheck.Message, tokenCheck.Message);
                }
                if (addMultipleApartmentDto==null)
                {
                    return new ErrorDataResult<object>(null, "Alanlar boş geçilemez", Messages.err_null);
                }
                foreach (var item in addMultipleApartmentDto.Apartments)
                {
                    _apartmentDal.Add(new Apartment
                    {
                        WhichBlock = item.WhichBlock,
                        ApartmentNo = item.ApartmentNo,
                        FloorNumber = item.FloorNumber,
                        ApartmentSize = item.ApartmentSize,
                        ApartmentState=item.ApartmentState,
                        Status=item.Status
                    });
                }
                var count = addMultipleApartmentDto.Apartments.Count();
                return new SuccessDataResult<object>("Ok", $"{count} tane daire kaydı oluştu.", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<object>(null,e.Message, Messages.unknown_err);

            }
        }

        public IDataResult<bool> Delete(int id, string token)
        {
            
            try
            {
                var tokenCheck = _sessionService.CheckAllControls(token, Permission.per_delapartment);
                if (tokenCheck==null)
                {
                    return new ErrorDataResult<bool>(false, tokenCheck.Message, tokenCheck.MessageCode);
                }
                if (id!= null)
                {
                    var result = _apartmentDal.Get(x => x.Id == id);
                    _apartmentDal.Delete(result);
                    return new SuccessDataResult<bool>(true, "kayıt silindi", Messages.success);

                }
                return new ErrorDataResult<bool>(false, "Alan boş geçilemez", Messages.err_null);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<bool>(false, e.Message, Messages.unknown_err);

            }
        }

        public IDataResult<Apartment> Get(Expression<Func<Apartment, bool>> filter)
        {
            try
            {
                var getApartment = _apartmentDal.Get(filter);
                if (getApartment ==null)
                {
                    return new ErrorDataResult<Apartment>(null, "apartment not found", Messages.data_not_found);
                }
                return new SuccessDataResult<Apartment>(getApartment, "OK", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<Apartment>(null, e.Message, Messages.unknown_err);

            }
        }

        public IDataResult<ApartmentListDto> GetById(int id, string token)
        {
            var tokenCheck = _sessionService.CheckAllControls(token, Permission.per_getbyiduser);
            try
            {
                var apartment = _apartmentDal.Get(x => x.Id == id);

                if (apartment==null || id==null)
                {
                    return new ErrorDataResult<ApartmentListDto>(null, "Data not found", Messages.data_not_found);
                }
                var apartmentDto = new ApartmentListDto()
                {
                    WhichBlock = apartment.WhichBlock,
                    ApartmentNo = apartment.Id,
                    FloorNumber = apartment.FloorNumber,
                    ApartmentSize = apartment.ApartmentSize,
                    ApartmentState = apartment.ApartmentState,
                    Status = apartment.Status,

                };
                return new SuccessDataResult<ApartmentListDto>(apartmentDto, "Ok", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<ApartmentListDto>(new ApartmentListDto(), e.Message, Messages.unknown_err);

            }
        }

        public IDataResult<ApartmentListDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<ApartmentListDto>> GetList()
        {
            try
            {
               var apartments=_apartmentDal.GetList();
                var apartmentsDto = new List<ApartmentListDto>();

                foreach (var apartment in apartments)
                {
                    apartmentsDto.Add(new ApartmentListDto
                    {
                        Id = apartment.Id,
                        WhichBlock = apartment.WhichBlock,
                        ApartmentNo = apartment.ApartmentNo,
                        FloorNumber = apartment.FloorNumber,
                        ApartmentSize = apartment.ApartmentSize,
                        ApartmentState=apartment.ApartmentState,
                        Status=apartment.Status,

                    });
                }
                return new SuccessDataResult<List<ApartmentListDto>>(apartmentsDto, "Ok", Messages.success);
             
            }
            catch (Exception E)
            {

                return new ErrorDataResult<List<ApartmentListDto>>(new List<ApartmentListDto>(), E.Message, Messages.unknown_err);
            }
        }

        public IDataResult<ApartmentUpdateDto> Update(ApartmentUpdateDto apartmentUpdateDto)
        {
            try
            {
                var result = _apartmentDal.Get(x => x.Id == apartmentUpdateDto.Id);
                var returnDto = new ApartmentUpdateDto(); //amacım geri dönüşte null olmayan propertylerin kullanıcııya gösterilmesi fakat işe yaramadı

                
                returnDto.Id=apartmentUpdateDto.Id;
                if (result != null)
                {
                    if (apartmentUpdateDto==null)
                    {
                        return new ErrorDataResult<ApartmentUpdateDto>(null, "Güncelleme işlemi yapılamadı", Messages.err_null);
                    }
                    if (apartmentUpdateDto.WhichBlock != null)
                    {
                        result.WhichBlock = apartmentUpdateDto.WhichBlock;
                    }
                    if (apartmentUpdateDto.Status != null)
                    {
                        result.Status= apartmentUpdateDto.Status.Value;
                    }
                    if (apartmentUpdateDto.ApartmentState != null)
                    {
                        result.ApartmentState = apartmentUpdateDto.ApartmentState.Value;
                    }
                    if (apartmentUpdateDto.ApartmentSize != null)
                    {
                        result.ApartmentState = apartmentUpdateDto.ApartmentState.Value;
                    }
                    if (apartmentUpdateDto.ApartmentNo != null)
                    {
                        result.ApartmentNo = apartmentUpdateDto.ApartmentNo.Value;
                    }
                    if (apartmentUpdateDto.FloorNumber != null)
                    {
                        result.FloorNumber = apartmentUpdateDto.FloorNumber.Value;
                    }
                    {

                    }
                    _apartmentDal.Update(result);
                    return new SuccessDataResult<ApartmentUpdateDto>(apartmentUpdateDto , "Daire bilgileri güncellendi", Messages.success);

                }
                return new ErrorDataResult<ApartmentUpdateDto>(null, "Err", Messages.err_null);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<ApartmentUpdateDto>(null, e.Message, Messages.unknown_err);
            }
        }
    }
}
