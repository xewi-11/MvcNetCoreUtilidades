using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace MvcNetCoreUtilidades.Controllers
{
    public class MailsController : Controller
    {
        private IConfiguration configuration;
        public MailsController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IActionResult SendMail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendMail(string to, string asunto, string mensaje)
        {
            string user = this.configuration.GetValue<string>("MailSettings:Credentials:User");
            //OBJETO PARA LA INFORAMACION DEL MAIL
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(user);
            mail.To.Add(to);
            mail.Subject = asunto;
            mail.Body = mensaje;
            //<h1>Hola</h1>
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            //RECUPERAMOS LOS DATOS PARA EL OBJETO QUE MANDA EL PROPIO MAIL
            string password = this.configuration.GetValue<string>("MailSettings:Credentials:Password");
            string host = this.configuration.GetValue<string>("MailSettings:Server:Host");
            int port = this.configuration.GetValue<int>("MailSettings:Server:Port");
            bool ssl = this.configuration.GetValue<bool>("MailSettings:Server:Ssl");
            bool defailtCredentials = this.configuration.GetValue<bool>("MailSettings:Server:DefaultCredentials");
            SmtpClient client = new SmtpClient();
            client.Host = host;
            client.Port = port;
            client.EnableSsl = ssl;
            client.UseDefaultCredentials = defailtCredentials;
            client.Credentials = new NetworkCredential(user, password);
            ViewData["MENSAJE"] = "Mail enviado a " + to;
            await client.SendMailAsync(mail);
            return View();
        }
    }
}
