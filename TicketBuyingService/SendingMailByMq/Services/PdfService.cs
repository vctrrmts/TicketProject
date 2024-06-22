using QRCoder;
using SendingMailByMq.Models;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace SendingMailByMq.Services;

public static class PdfService
{
    public static string[] GenerateTicketPdf(Ticket[] tickets)
    {
        var ticketPaths = new string[tickets.Length];
        for (int i = 0; i < tickets.Length; i++)
        {
            // Initialize document object
            Document document = new Document();
            // Add page
            Page page = document.Pages.Add();

            // -------------------------------------------------------------
            // Add image
            var imageFilePath = GenerateQrCode(tickets[i].HashGuid, tickets[i].TicketId.ToString());
            page.AddImage(imageFilePath, new Aspose.Pdf.Rectangle(415, 640, 575, 800));

            //-------------------------------------------------------------
            //Add Header
            var header = new TextFragment(tickets[i].Event.Name);
            header.TextState.FontSize = 24;
            header.HorizontalAlignment = HorizontalAlignment.Center;
            header.Position = new Position(15, 760);
            page.Paragraphs.Add(header);

            var convertedDate = DateTime.SpecifyKind(tickets[i].Event.DateTimeEventStart, DateTimeKind.Utc);
            var localDateTimeEvent = convertedDate.ToLocalTime();

            // Add description
            var descriptionText = $"{tickets[i].Event.Location.Name}\n" +
                $"{tickets[i].Event.Location.City.Name}, {tickets[i].Event.Location.Address}\n" +
                $"{localDateTimeEvent.ToString("dd.MM.yyyy")}  {localDateTimeEvent.ToString("HH:mm")}\n";
            var description = new TextFragment(descriptionText);
            description.TextState.FontSize = 16;
            description.HorizontalAlignment = HorizontalAlignment.Left;
            page.Paragraphs.Add(description);

            var seatText = tickets[i].Seat.Row is null ? "" : $"Ряд: {tickets[i].Seat.Row}    Место: {tickets[i].Seat.SeatNumber}";
            var fullSeatText = $"Сектор: {tickets[i].Seat.Sector}    " + seatText;
            var seat = new TextFragment(fullSeatText);
            //seat.TextState.Font = FontRepository.FindFont("Times New Roman");
            seat.TextState.FontSize = 16;
            seat.TextState.FontStyle = FontStyles.Bold;
            seat.HorizontalAlignment = HorizontalAlignment.Left;
            page.Paragraphs.Add(seat);

            var ticketPath = Path.Combine(Environment.CurrentDirectory, $"ticket{i + 1}.pdf");
            document.Save(ticketPath);
            ticketPaths[i] = ticketPath;

            File.Delete(imageFilePath);
        }
        
        return ticketPaths;
    }

    private static string GenerateQrCode(string content, string filename)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, filename + ".jpg");

        using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
        using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q))
        using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
        {
            byte[] qrCodeImageBytes = qrCode.GetGraphic(10);
            SaveBytesToPng(qrCodeImageBytes, filePath);
        }

        return filePath;
    }

    private static void SaveBytesToPng(byte[] imageBytes, string fileName)
    {
        using (MemoryStream ms = new MemoryStream(imageBytes))
        {
            using (var image = SixLabors.ImageSharp.Image.Load<Rgba32>(ms))
            {

                JpegEncoder jp = new JpegEncoder()
                {
                    Quality = 100,
                    ColorType = JpegEncodingColor.YCbCrRatio420
                };

                image.SaveAsJpeg(fileName, jp);
            }
        }
    }
}
