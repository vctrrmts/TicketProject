using FluentValidation;

namespace Notification.Application.Handlers.Queries.GetEvents;

public class GetEventsQueryValidator : AbstractValidator<GetEventsQuery>
{
    public GetEventsQueryValidator() { }
}
