using FluentValidation;

namespace TicketEventSearch.Application.Handlers.Cities.Commands.DeleteCity;

public class DeleteCityCommandValidator : AbstractValidator<DeleteCityCommand>
{
    public DeleteCityCommandValidator()
    {
        RuleFor(x => x.CityId).NotEmpty();
    }
}
