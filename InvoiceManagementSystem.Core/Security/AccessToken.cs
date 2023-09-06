using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Core.Security
{
    public class AccessToken //1-)ERİŞİM TOKENINI VE SÜRESİNİ İÇERİR
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string StampExpiration { get; set; }
    }
}
