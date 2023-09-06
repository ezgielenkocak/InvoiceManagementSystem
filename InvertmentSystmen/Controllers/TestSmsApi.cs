using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Threading.Tasks;

namespace InvoiceManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestSmsApi : ControllerBase
    {
        [HttpGet("testsendsms")]
        public async Task<IActionResult> TestSendEmail()
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("elen-kocak@hotmail.com", "Ezgi Elen Koçak");
            var subject = "SendGrid ile Test Email'i Saygılar";
            var to = new EmailAddress("elen.kocak@coino.com", "Dear Elen");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return Ok(new { Message = "Mail başarıyla yollandı" });
        }
    }
}
