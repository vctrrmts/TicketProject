using FluentValidation;

namespace TicketEventSearch.Application.Handlers.Cities.Queries.GetCity;

public class GetCityByIdQueryValidator : AbstractValidator<GetCityByIdQuery>
{
    public GetCityByIdQueryValidator()
    {
        RuleFor(x => x.CityId).NotEmpty();
    }
}
