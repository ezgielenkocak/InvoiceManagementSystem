using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using InvoiceManagementSystem.Core.Entities.Concrete;
using InvoiceManagementSystem.Core.Result;
using InvoiceManagementSystem.Entity.Concrete;
using InvoiceManagementSystem.Entity.Dtos;
using InvoiceManagementSystem.Entity.Dtos.ApartmentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.BLL.Abstract
{
    public interface IApartmentService
    {
        IDataResult<object> Add(AddMultipleApartmentDto addMultipleApartmentDto);
        IDataResult<ApartmentUpdateDto> Update(ApartmentUpdateDto apartmentUpdateDto);
        IDataResult<bool> Delete(int id, string token);
        IDataResult<List<ApartmentListDto>> GetList();
        IDataResult<ApartmentListDto> GetById(int id);
        IDataResult<Apartment> Get(Expression<Func<Apartment, bool>> filter);

    }
}
