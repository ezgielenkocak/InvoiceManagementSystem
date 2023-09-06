using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Core.Security
{
    public class TokenOptions //3-)JWT token seçeneklerini içerir, örneğin token'ın geçerlilik süresi, yayıncı bilgisi ve alıcı bilgisi gibi.
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpriation { get; set; }
        public string   SecurityKey { get; set; }
    }
}
