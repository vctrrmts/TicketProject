using EventNotifyApp.ExternalProviders;
using EventNotifyApp.Services;
using System.Timers;
using Timer = System.Timers.Timer;

class Program
{
    private static string login = string.Empty;
    private static string password = string.Empty;
    static void Main()
    {
        TryAuthorize();

        Timer timer = new Timer();
        int intervalInMinutes = 20;
        timer.Interval = intervalInMinutes * 60000;
        timer.Elapsed += Timer_Elapsed;
        timer.Start();

        Console.ReadKey();
        timer.Stop();
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
                var accessToken = await authProvider.CreateJwtTokenAsync(login, password);
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

    static async void TryAuthorize()
    {
        bool authorized = false;

        using (var client = new HttpClient())
        {
            while (authorized == false)
            {
                Console.WriteLine("Login: ");
                login = Console.ReadLine()!;
                Console.WriteLine("Password: ");
                password = Console.ReadLine()!;

                try
                {
                    AuthProvider authProvider = new AuthProvider(client);
                    var accessToken = await authProvider.CreateJwtTokenAsync(login, password);
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        authorized = true;
                        Console.Clear();
                        Console.WriteLine("Authorized.");
                        Console.WriteLine("Press any key to leave.");
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine($"{ex.Message}");

                }
            }
        }
    }
}