using InvoiceManagementSystem.Core.Entities.Concrete;
using InvoiceManagementSystem.Core.Result;
using InvoiceManagementSystem.Entity.Dtos.AuthDtos;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Dtos
{
    public class SendEmailDto
    {
        public static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length) //belirtilen bir nesneyi belirtilen sayıda kez içeren bir IEnumerable koleksiyonu oluşturur. Yani, bu metodu kullanarak bir karakter dizisini veya başka bir nesneyi belirtilen sayıda tekrarlayarak yeni bir koleksiyon oluşturabilirsin

              .Select(s => s[random.Next(s.Length)]).ToArray()); // koleksiyondaki her bir karakteri, koleksiyonun uzunluğu içinde rasgele bir karakter seçerek değiştirir. //ToArray() ifadesi, koleksiyonu karakter dizisine dönüştürür.
        }
        public async Task<Response> SendEmail(string toemail, string usersName,string password)
        {

            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("elen-kocak@hotmail.com", "Ezgi Elen Koçak");
            var subject = "SendGrid ile Test Email'i Saygılar";
            var to = new EmailAddress(toemail, "Dear " + usersName);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = $"ElenKocak Sitesi Sakini Aramıza Hoşgeldin! <br> Şifren:</strong>{password}";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return response;
        }
    }
}
