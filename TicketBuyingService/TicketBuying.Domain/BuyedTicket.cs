using System.Globalization;
using System.Text.RegularExpressions;

namespace TicketBuying.Domain
{
    public class BuyedTicket
    {
        public Guid TicketId { get; private set; }
        public Guid EventId { get; private set; }
        public double Price { get; private set; }
        public string ClientMail { get; private set; } 
        public string HashGuid { get; private set; }

        private BuyedTicket() { }

        public BuyedTicket(Guid ticketId, Guid eventId, double price, string clientMail, string hashGuid)
        {
            if (string.IsNullOrWhiteSpace(ticketId.ToString()))
            {
                throw new ArgumentException("Incorrect TicketId", nameof(ticketId));
            }

            if (string.IsNullOrWhiteSpace(eventId.ToString()))
            {
                throw new ArgumentException("Incorrect EventId", nameof(eventId));
            }

            if (price <= 0)
            {
                throw new ArgumentException("Incorrect Price", nameof(price));
            }

            if (!IsValidEmail(clientMail))
            {
                throw new ArgumentException("Incorrect Mail Address", nameof(clientMail));
            }

            if (string.IsNullOrWhiteSpace(hashGuid))
            {
                throw new ArgumentException("Incorrect HashGuid", nameof(hashGuid));
            }

            TicketId = ticketId;
            EventId = eventId;
            Price = price;
            ClientMail = clientMail;
            HashGuid = hashGuid; 
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
