using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Core.Result
{
    public class ErrorDataResult<T>:DataResult<T>
    {
        public ErrorDataResult(T data):base(data,true)
        {

        }
        public ErrorDataResult(T data, string message, string messageCode):base( true, message, messageCode,data )
        {

        }
    }
}
