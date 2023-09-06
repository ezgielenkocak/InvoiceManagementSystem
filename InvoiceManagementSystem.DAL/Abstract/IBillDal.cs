using InvoiceManagementSystem.Core.Repository;
using InvoiceManagementSystem.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.DAL.Abstract
{
    public interface IBillDal : IEntityRepository<Bill>
    {
    }
}
