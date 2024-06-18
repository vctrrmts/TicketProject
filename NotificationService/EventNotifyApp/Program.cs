using EventNotifyApp.ExternalProviders;
using EventNotifyApp.Services;
using System.Configuration;
using System.Timers;
using Timer = System.Timers.Timer;

class Program
{
    static void Main()
    {
        Timer timer = new Timer();
        int intervalInMinutes = 5;
        timer.Interval = intervalInMinutes * 60000;
        timer.Elapsed += Timer_Elapsed;
        timer.Start();

        Console.Read();
    }

    static async void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        using (var client = new HttpClient())
        {
            try
            {
                #region Get all events with time start next 2 hours 
                EventsRepository eventsRepository = new EventsRepository(client);
                var eventsFromSearchService = await eventsRepository.GetEventsFromSearchServiceAsync();

                eventsFromSearchService = eventsFromSearchService.Where(x=>x.DateTimeEventStart > DateTime.UtcNow 
                && x.DateTimeEventStart < DateTime.UtcNow.AddHours(2)).ToArray();

                if (eventsFromSearchService.Length == 0) return;
                #endregion

                #region Check is notification have been sent
                AuthProvider authProvider = new AuthProvider(client);
                var accessToken = await authProvider.CreateJwtTokenAsync(
                    ConfigurationManager.AppSettings["LoginJwt"]!, 
                    ConfigurationManager.AppSettings["PasswordJwt"]!);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var notifiedEvents = await eventsRepository.GetNotifiedEventsAsync();
                var notifiedEventsIds = notifiedEvents.Select(x=>x.EventId);

                eventsFromSearchService = eventsFromSearchService.Where(x => !notifiedEventsIds.Contains(x.EventId)).ToArray();

                if (eventsFromSearchService.Length == 0) return;
                #endregion

                #region Send notification to all clients of all not notified events
                TicketsProvider ticketsProvider = new TicketsProvider(client);
                foreach (var myEvent in eventsFromSearchService)
                {
                    var tickets = (await ticketsProvider.GetBuyedTicketsByEventAsync(myEvent.EventId))
                        .DistinctBy(x=>x.ClientMail);

                    foreach (var ticket in tickets)
                    {
                        await EmailService.SendEmailAsync(myEvent, ticket.ClientMail);
                    }
                    Console.WriteLine($"{DateTime.Now} Notifications on '{myEvent.Name}' sent out.");

                    await eventsRepository.AddNotifiedEventAsync(myEvent.EventId);
                }
                #endregion

            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + " " + ex.Message);
            }
        }
    }
}