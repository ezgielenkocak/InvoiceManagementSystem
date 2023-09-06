using InvoiceManagementSystem.Core.Entities.Concrete;
using InvoiceManagementSystem.Core.EntityFramework;
using InvoiceManagementSystem.DAL.Abstract;
using InvoiceManagementSystem.DAL.Concrete.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.DAL.Concrete.EntityFramework
{
    public class EfUserDal:EfEntityRepositoryBase<User, ImsDbContext>,IUserDal
    {
        public List<OperationClaim> GetClaims(User users)
        {
            using (var context=new ImsDbContext())
            {
                var result = from operationClaim in context.Roles
                             join userOperationClaim in context.UserRoles
                             on operationClaim.Id equals userOperationClaim.RoleId
                             where userOperationClaim.UserId == users.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();
            }
        }
    }
}
