using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Core.Security
{
    public class SecurityKeyHelper //2-)Belirtilen bir güvenlik anahtarından bir Security Key nesnesi oluşturur.
    {
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }

        #region  SecurityKeyHelper classı
        // JWT kullanarak güvenli bir şekilde bilgi taşımak için, bir (security key) gereklidir.
        // Güvenlik anahtarı, bir """JWT'yi imzalamak"""" ve """doğrulamak""" için kullanılır. Bu anahtar, bir simetrik veya asimetrik anahtar olabilir.
        //SymmetricSecurityKey, bir simetrik anahtarı temsil eder ve bu anahtar, hem JWT oluştururken hem de doğrularken kullanılacaktır.
        #endregion

        #region CreateSecurityKey() Metodu
        // CreateSecurityKey() metodu;
        // Verilen güvenlik anahtarını UTF-8 kodlama kullanarak byte dizisine dönüştürür.
        // Daha sonra, bu byte dizisini kullanarak bir SymmetricSecurityKey nesnesi oluşturur ve bu nesneyi geri döndürür.
        #endregion
    }
}
