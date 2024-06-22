using QRCoder;
using SendingMailByMq.Models;
using System.Configuration;
using System.Net;
using System.Net.Mail;


namespace SendingMailByMq.Services;

public static class EmailService
{
    public static void SendEmailAsync(Ticket[] tickets)
    {
        string mailFrom = ConfigurationManager.AppSettings["MailFrom"]!;
        MailAddress from = new MailAddress(mailFrom, "Ticket Service");
        MailAddress to = new MailAddress(tickets[0].Mail);

        MailMessage m = new MailMessage(from, to);
        m.Subject = "Tickets for the event " + tickets[0].Event.Name + "";
        m.Body = "Thank you for your purchase!";

        var pdfPaths = PdfService.GenerateTicketPdf(tickets);

        foreach (var pdfPath in pdfPaths)
        {
            var attachment = new Attachment(pdfPath);
            m.Attachments.Add(attachment);
        }



        m.IsBodyHtml = false;

        SmtpClient smtp = new SmtpClient("smtp.mail.ru", 2525);
        smtp.Credentials = new NetworkCredential(mailFrom, ConfigurationManager.AppSettings["MailServicePassword"]!);
        smtp.EnableSsl = true;
        smtp.Send(m);

        foreach (var pdfPath in pdfPaths)
        {
            File.Delete(pdfPath);
        }
    }
}
