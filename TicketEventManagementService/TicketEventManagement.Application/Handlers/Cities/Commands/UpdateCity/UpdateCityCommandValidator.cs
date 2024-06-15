using FluentValidation;

namespace TicketEventManagement.Application.Handlers.Cities.Commands.UpdateCity;

public class UpdateCityCommandValidator : AbstractValidator<UpdateCityCommand>
{
    public UpdateCityCommandValidator()
    {
        RuleFor(x => x.CityId).NotEmpty();
        RuleFor(x => x.Name).Length(1, 50).NotEmpty();
    }
}
