using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using InvoiceManagementSystem.Core.Entities.Concrete;
using InvoiceManagementSystem.Core.Result;
using InvoiceManagementSystem.Entity.Concrete;
using InvoiceManagementSystem.Entity.Dtos.BillDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.BLL.Abstract
{
    public interface IBillService
    {
        IDataResult<object> Add(AddMultipleBillDto addMultipleBillDto);
        IDataResult<bool> Update(UpdateBillDto updateBillDto);
        IDataResult<bool> UpdateIsBillPaymentStatus(int id);
        IDataResult<List<ListBillDto>> GetList();
        IDataResult<ListBillDto> GetById(int id);
        IDataResult<bool>Delete(int id);
        IDataResult<Bill> Get(Expression<Func<Bill, bool>> filter);

        IDataResult<BillListPagingDto> GetListWithPaging(BillGetlistFilterDto billGetlistFilterDto);

    }
}
