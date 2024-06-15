using FluentValidation;

namespace TicketEventManagement.Application.Handlers.Locations.Queries.GetLocation;

public class GetLocationByIdQueryValidator : AbstractValidator<GetLocationByIdQuery>
{
    public GetLocationByIdQueryValidator()
    {
        RuleFor(x => x.LocationId).NotEmpty();
    }
}
