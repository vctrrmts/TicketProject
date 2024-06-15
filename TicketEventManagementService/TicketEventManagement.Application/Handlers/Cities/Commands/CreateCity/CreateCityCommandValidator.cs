using FluentValidation;

namespace TicketEventManagement.Application.Handlers.Cities.Commands.CreateCity;

public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
{
    public CreateCityCommandValidator()
    {
        RuleFor(x => x.Name).Length(1, 50).NotEmpty();
    }
}
