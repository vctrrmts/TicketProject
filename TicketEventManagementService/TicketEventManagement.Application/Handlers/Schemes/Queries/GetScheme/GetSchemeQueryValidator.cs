using FluentValidation;

namespace TicketEventManagement.Application.Handlers.Schemes.Queries.GetScheme;

public class GetSchemeQueryValidator : AbstractValidator<GetSchemeQuery>
{
    public GetSchemeQueryValidator() 
    {
        RuleFor(e=>e.SchemeId).NotEmpty();
    }
}
