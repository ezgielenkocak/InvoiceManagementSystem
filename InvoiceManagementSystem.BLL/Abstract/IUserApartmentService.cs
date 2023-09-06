using InvoiceManagementSystem.Core.Result;
using InvoiceManagementSystem.Entity.Dtos.UserApartmentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.BLL.Abstract
{
    public interface IUserApartmentService
    {
        IDataResult<bool> Add(UserApartmentAddDto userApartmentAddDto);
        IDataResult<bool> Update(UserApartmentUpdateDto userApartmentUpdateDto);
        IDataResult<bool> Delete(int id);
        IDataResult<UserApartmentListDto> GetById(int id);
        IDataResult<List<UserApartmentListDto>> GetList();
        IDataResult<List<UserApartmentListDto>> ActiveUserGetList();
        IDataResult<UserApartmentAddMultipleDto> AddMultiple(UserApartmentAddMultipleDto dto);
        //IDataResult<List<UserApartmentListDto>> PassiveUserGetList(); //taşınanlar
    }
}
