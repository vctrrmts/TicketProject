using Notification.Domain;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace EventNotifyApp.Services;

public static class EmailService
{
    public static async Task SendEmailAsync(Event myEvent, string mail)
    {
        try
        {
            string mailFrom = ConfigurationManager.AppSettings["MailFrom"]!;
            MailAddress from = new MailAddress(mailFrom, "Ticket Service");
            MailAddress to = new MailAddress(mail);

            MailMessage m = new MailMessage(from, to);
            m.Subject = $"Reminder about the \"{myEvent.Name}\" event";
            m.Body = $"The event will take place on {myEvent.DateTimeEventStart.ToString("M")} at {myEvent.DateTimeEventStart.ToString("HH.mm")}." +
                $"\r\nVenue: {myEvent.Location.Name}." +
                $"\r\nAddress: {myEvent.Location.Address}." +
                $"\r\n\r\nHave a good time!";

            m.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 2525);
            smtp.Credentials = new NetworkCredential(mailFrom, ConfigurationManager.AppSettings["MailServicePassword"]!);
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(m);
        }
        catch (Exception ex)
        {
            Console.WriteLine(DateTime.Now + " " + ex.Message);
        }

    }
}
