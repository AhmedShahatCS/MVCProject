using MVCProject.DAL.Entities;
using System.Net;
using System.Net.Mail;

namespace MVCProject.PL.Helpers
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client= new SmtpClient("smtp.gmail.com",587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("ggndgm@gmail.com", "vqkmwivaxznjllap");
            client.Send("ggndgm@gmail.com", email.To, email.Subject, email.body);
        }
    }
}
