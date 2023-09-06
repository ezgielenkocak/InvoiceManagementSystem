using InvoiceManagementSystem.Core.Entities.Concrete;
using InvoiceManagementSystem.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.DAL.Abstract
{
    public interface IUserDal:IEntityRepository<User>
    {
    }
}
