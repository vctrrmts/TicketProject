using FluentValidation;

namespace TicketEventSearch.Application.Handlers.Cities.Commands.CreateCity;

public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
{
    public CreateCityCommandValidator()
    {
        RuleFor(x => x.Name).Length(1, 50).NotEmpty();
        RuleFor(x => x.CityId).NotEmpty();
    }
}
