using FluentValidation;

namespace TicketEventSearch.Application.Handlers.Locations.Commands.UpdateLocation;

public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
{
    public UpdateLocationCommandValidator()
    {
        RuleFor(x => x.LocationId).NotEmpty();
        RuleFor(x => x.Name).Length(1, 200).NotEmpty();
        RuleFor(x => x.Address).Length(1, 200).NotEmpty();
        RuleFor(x => x.Latitude).GreaterThanOrEqualTo(-90).LessThanOrEqualTo(90);
        RuleFor(x => x.Longitude).GreaterThanOrEqualTo(-180).LessThanOrEqualTo(180);
        RuleFor(x => x).NotEmpty();
    }
}
