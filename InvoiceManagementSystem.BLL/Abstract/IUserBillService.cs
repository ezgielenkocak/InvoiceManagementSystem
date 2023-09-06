using InvoiceManagementSystem.Core.Result;
using InvoiceManagementSystem.Entity.Concrete;
using InvoiceManagementSystem.Entity.Dtos.BillDtos;
using InvoiceManagementSystem.Entity.Dtos.UserBillDtos;
using InvoiceManagementSystem.Entity.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.BLL.Abstract
{
    public interface IUserBillService
    {
        IDataResult<UserBillAddDto> Add(UserBillAddDto userBillAdd);
        IDataResult<UserBillMultipleAddDto> AddMultiple(UserBillMultipleAddDto userBillMultipleAdd);
        IDataResult<UserBillUpdateDto> Update(UserBillUpdateDto userUpdateDto);
        IDataResult<UserBill> Delete(int id);   
        IDataResult<UserBill> DeletebyPassive(int id);
        IDataResult<List<UserBillListDto>> GetList();
        IDataResult<List<UserBillListDto>> GetActiveBills();
        IDataResult<List<UserBillListDto>> GetPassiveBills();
        IDataResult<UserBillListDto> GetById(int id);
        IDataResult<UserBill> Get(Expression<Func<UserBill, bool>> filter);
        IDataResult<List<UserBillListDto>> BillsPayable(string token);

    }
}
