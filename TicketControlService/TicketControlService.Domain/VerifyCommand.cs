namespace TicketControlService.Domain;

public class VerifyCommand
{
    public string HashGuid { get; set; } = default!;
}
