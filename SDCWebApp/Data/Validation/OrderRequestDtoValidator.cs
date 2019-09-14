using FluentValidation;
using SDCWebApp.Models.Dtos;

namespace SDCWebApp.Data.Validation
{
    public class OrderRequestDtoValidator : AbstractValidator<OrderRequestDto>, ICustomValidator<OrderRequestDto>
    {
        public OrderRequestDtoValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Customer)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .SetValidator(new CustomerDtoValidator());

            RuleForEach(x => x.Tickets)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .SetValidator(new ShallowTicketValidator(dbContext));
        }
    }
}
