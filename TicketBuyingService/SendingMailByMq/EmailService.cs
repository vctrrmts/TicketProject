using QRCoder;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Mail;
using TicketBuying.Domain;


namespace SendingMailByMq;

public static class EmailService
{
    public static string GenerateQrCode(string content)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
        QRCode qrCode = new QRCode(qrCodeData);
        Bitmap qrCodeImage = qrCode.GetGraphic(10);

        string folderPath = Path.Combine(Environment.CurrentDirectory, "QrCodes");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string fileName = Path.Combine(folderPath, content +".png");
        qrCodeImage.Save(fileName, ImageFormat.Png);

        return fileName;
    }
    public static void SendEmailAsync(Ticket ticket)
    {
        string mailFrom = ConfigurationManager.AppSettings["MailFrom"]!;
        MailAddress from = new MailAddress(mailFrom, "Ticket Service");
        MailAddress to = new MailAddress(ticket.Mail);

        MailMessage m = new MailMessage(from, to);
        m.Subject = "Tickets for the event " + ticket.Event.Name + "";
        m.Body = "Thank you for your purchase!";

        //var attachment = new Attachment("path");
        //m.Attachments.Add(attachment);
        // ТУТ по сути я прикреплю сформированный pdf билет, но пока нет времени заняться его заполнением

        m.IsBodyHtml = false;

        SmtpClient smtp = new SmtpClient("smtp.mail.ru", 2525);
        smtp.Credentials = new NetworkCredential(mailFrom, ConfigurationManager.AppSettings["MailServicePassword"]!);
        smtp.EnableSsl = true;
        smtp.Send(m);
    }
}
