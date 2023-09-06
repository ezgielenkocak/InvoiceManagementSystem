using InvoiceManagementSystem.Core.Entities.Concrete;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Core.Security
{
    public interface ITokenHelper //4-)AccessToken nesnelerini ve SessionAddDto nesnelerini oluşturmak için metotları içerir
    {

       // Bu işlem, öncelikle bir JWT token oluşturur, sonra bu token'ı belirli bir şifreleme yöntemi kullanarak kriptografi algoritmasına tabi tutar ve son olarak bir Access Token nesnesi oluşturur.
        AccessToken CreateToken(User user, List<OperationClaim> claims); // kullanıcının kimlik bilgilerini ve yetkilerini kullanarak bir AccessToken nesnesi oluşturur.



        //CreateNewToken metodu, kullanıcının kimlik bilgilerini kullanarak bir SessionAddDto nesnesi oluşturur. Bu işlem, öncelikle bir JWT token oluşturur, sonra bu token'ı belirli bir şifreleme yöntemi kullanarak kriptografi algoritmasına tabi tutar ve son olarak bir SessionAddDto nesnesi oluşturur.
        SessionAddDto CreateNewToken(User user);
    }
}
