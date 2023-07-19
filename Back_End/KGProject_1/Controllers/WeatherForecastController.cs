using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using KGProject_1.Models.Entities;

namespace KGProject_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailInfo
    {
        public string email;
        public string tieude;
        public string noidung;
    }
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        
   

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("SendEmail")]
        public void Get2([FromBody] Img data)
        {

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("dragon2112sss@gmail.com");
            mailMessage.To.Add(new MailAddress(data.Id));
            mailMessage.Subject = data.Tid;
            mailMessage.Body = data.Urlimg;
            mailMessage.IsBodyHtml = true;
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("dragon2112sss@gmail.com", "cuqegmpwmywduxrv"),
                EnableSsl = true
            };

            // Send the email
            smtpClient.Send(mailMessage);
        }


        [HttpGet]
        [Route("getok")]
        public void Get2()
        {
            //SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            //smtpClient.Port = 44344;
            //smtpClient.Credentials = new NetworkCredential("dragon2112sss@gmail.com", "cuqegmpwmywduxrv");
            //smtpClient.EnableSsl = true;


            // Create a new MailMessage
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("dragon2112sss@gmail.com");
            mailMessage.To.Add(new MailAddress("phantientrong2112ss@gmail.com"));
            mailMessage.Subject = "Hello, world!";
            mailMessage.Body = "This is the content of the email.";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("dragon2112sss@gmail.com", "cuqegmpwmywduxrv"),
                EnableSsl = true
            };

            // Send the email
            smtpClient.Send(mailMessage);
        }
    }
}
