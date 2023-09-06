using InvoiceManagementSystem.Core.EntityFramework;
using InvoiceManagementSystem.DAL.Abstract;
using InvoiceManagementSystem.DAL.Concrete.Context;
using InvoiceManagementSystem.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.DAL.Concrete.EntityFramework
{
    public class EfPermissionsDal:EfEntityRepositoryBase<Permissions, ImsDbContext>, IPermissionsDal
    {
    }
}
