using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Core.Security
{
    public class HashingHelper
    {
        #region out
        /* Eğer out anahtar kelimesi kullanılmadan sadece return ile passwordhash ve passwordsalt döndürülürse, bu sefer CreatePasswordHash metodunu kullanmak isteyenler sadece passwordhash ve passwordsalt'ın ikisini birden alabilirler. Ancak out anahtar kelimesi kullanılarak, hem passwordhash hem de passwordsalt ayrı ayrı döndürülebilir ve kullanıcılar ihtiyaçlarına göre sadece birini veya ikisini birden kullanabilirler.
         */
        #endregion
        #region Instance
        /*Instance, bir sınıfın örneği veya bir nesnesidir */
        #endregion
        #region Static metod tanımlama sebebim:
        /*CreatePasswordHash metodunun static olarak tanımlanmasının sebebi, bu metodun sınıfın bir instance'ı olmadan da kullanılabilmesidir. static metotlar, bir nesneye bağlı kalmadan çağrılabilirler ve nesne oluşturulmasını beklemeksizin doğrudan kullanılabilirler. Bu, daha kolay ve hızlı bir kullanım sağlar ve herhangi bir nesne  örneği oluşturmadan da metotlarımızı kullanmamızı sağlar. CreatePasswordHash metodunun, sınıfın diğer özellikleriyle herhangi bir bağı bulunmadığı için static olarak tanımlanması tercih edilir.
          */
        #endregion
        #region Static method kullanılma sebepleri
        /*
   Yaratılan nesne sayısını azaltmak: static metotlar, bir sınıfın tüm örneklerinde ortak olarak kullanılabilen işlemler için kullanılır. Eğer bir metot, sınıfın her örneği için aynı şekilde çalışıyorsa ve bir örneğe bağlı olmasına gerek yoksa, bu metodu static olarak tanımlamak daha mantıklı olacaktır. Bu şekilde, nesne oluşturulması gereksiz yere azaltılarak bellek kullanımı da azalmış olur.

   Metotların çağrılabilirliğini arttırmak: static metotlar, bir nesneye bağlı kalmadan direkt olarak sınıf adı ile çağrılabilirler. Bu sayede, new anahtar kelimesi ile nesne örneği oluşturmadan, sınıfın metotlarına doğrudan erişim sağlanabilir.

   Tekrar kullanılabilir kodlar yazmak: static metotlar, farklı yerlerde kullanılabilen ve tekrar eden işlemler için kullanılabilirler. Örneğin, bir dosya yazdırma metodu, sınıfın farklı noktalarında kullanılabilir ve bu metodun içerisindeki işlemler tekrar edilmeden tek bir yerde yazılabilir. Bu şekilde, kodun tekrarlanabilirliği azaltılır ve kodun bakımı daha kolay hale gelir.
         */
        #endregion
        #region Static kullanımı ve bellek durumu:

        /*
         *  static methodlar program başlatıldığında bellekte önceden ayrılmış olan bir bölgede yer alırlar ve bu bölge programın sonlanana kadar kullanımda kalır. Dolayısıyla, static methodlar diğer methodlardan farklı olarak herhangi bir örnek oluşturulmasına gerek kalmadan doğrudan sınıf adı üzerinden çağrılabilirler. Bu nedenle, statik methodlar nesne oluşturulmasını gerektirmediğinden bellekte daha az yer kaplarlar ve performans açısından daha avantajlıdırlar.
         */
        #endregion
        #region Bu metot ne işe yarıyor?
        /*
         * Bu metod, verilen bir şifre değerini HMAC-SHA256 algoritması kullanarak önce bir rastgele tuz değeri oluşturur ve ardından tuz değeri ve şifre değerini kullanarak bir şifre karma (hash) değeri hesaplar.

Metodun imzasında, password parametresi kullanıcının girdiği şifre değerini, passwordsalt ve passwordhash parametreleri ise metot tarafından hesaplanacak olan tuz değeri ve şifre karma değerini out anahtarı kullanarak geri döndürür.

HMACSHA256 sınıfı, SHA256 karma algoritması kullanarak bir özet (hash) değeri hesaplamak için bir anahtar (key) kullanır. Bu anahtar HMAC algoritmasının bir parçasıdır ve her uygulama çalıştığında farklı bir rastgele anahtar oluşturulur. Bu nedenle, aynı şifre değeri bile olsa, her hesaplama farklı bir şifre karma (hash) değeri üretir.

Kod bloğunda using anahtar kelimesi kullanılarak, HMACSHA256 sınıfının IDisposable arabirimini uygulamasına izin verilir. Bu sayede, sınıf kullanımı bittiğinde otomatik olarak bellekten atılması sağlanır. Bu işlem, programın bellek kullanımını optimize eder ve olası bellek sızıntılarını önler.
         */

        #endregion
        #region Password Salt ne işe yarıyor?
        /*
         * Tuz değeri (salt value), şifreleme işlemi sırasında şifreleme algoritmasına eklenen rastgele bir dizedir. Tuz, şifreleme işleminin güvenliğini artırmak için kullanılır. Aynı şifreleme algoritması kullanılarak farklı şifrelerin şifrelenmesi durumunda, tuzun her şifre için farklı olması, iki şifrenin aynı şifrelenmiş değere sahip olmasını önler. Bu, saldırganların şifrelenmiş verileri çözmesini daha zor hale getirir. Özetle, tuzlama, şifreleme işlemine rastgele bir değer eklemek suretiyle şifrelerin daha güvenli hale getirilmesini sağlar.
         */

        #endregion
        public static void CreatePasswordHash(string password, out byte[] passwordsalt, out byte[] passwordhash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

   
        public static bool VerifyPasswordHash(string password, byte[] passwordsalt, byte[] passwordhash)
        {
            using (var hmac=new System.Security.Cryptography.HMACSHA512(passwordsalt)) //password salt değerini kullanarak HMACSHA512 nesnesi oluşturulur
            {
                var computedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordhash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #region Metodun Amacı:
        /*
         * Bu metod, verilen şifrenin doğruluğunu doğrulamak için kullanılır.
         * Metod, önce verilen passwordsalt değeriyle yeni bir HMACSHA512 örneği oluşturur. Sonra, verilen şifreyi UTF8 formatında byte dizisine çevirir ve ComputeHash yöntemi ile hash'lenmiş bir değer hesaplar.

Daha sonra, hesaplanan hash değerinin her bir byte'ı için, passwordhash parametresindeki hash değerinin aynı pozisyondaki byte'ıyla karşılaştırma yapılır. Eğer iki değer eşleşmiyorsa, şifre doğrulanamamış demektir ve false değeri döndürülür. Eğer tüm byte'lar eşleşiyorsa, şifre doğrulanmıştır ve true değeri döndürülür.

Bu metod, bir şifrenin doğruluğunu doğrularken, hash'lenmiş şifre ve tuz değerini doğru şekilde karşılaştırmak için kullanılan bir yöntemdir.
         */
        #endregion
        #region Neden out kullanmadık?
        /*
         * CreatePasswordHash metodunda out anahtarını kullanmamızın nedeni, bu metotun 2 adet değer döndürmesi gerektiği içindir. Ancak VerifyPasswordHash metodunda, hesaplanan passwordsalt ve passwordhash değerleri zaten parametre olarak verilmiştir. Bu nedenle, out anahtarını kullanmak gerekli değildir. Yani, bu metodun görevi, verilen bir parolanın doğru olup olmadığını kontrol etmek ve sonucunu geri döndürmektir, hesaplama yapmak değil.
         */
        #endregion
        #region PasswordSalt
        /*
         * passwordsalt parametresi, CreatePasswordHash metodundan elde edilen ve parolayı hashlemek için kullanılan tuz değerini içermektedir. Yani CreatePasswordHash metodunda önce bir tuz değeri oluşturulup daha sonra bu tuz değeri, parolayı hashlemek için kullanılan algoritmanın yapısına göre bir şekilde saklanarak passwordsalt parametresine aktarılır. Daha sonra VerifyPasswordHash metodunda, doğru parola ve tuz değeri ile hash değerinin doğruluğu kontrol edilir.
         */
        #endregion
    }
}
