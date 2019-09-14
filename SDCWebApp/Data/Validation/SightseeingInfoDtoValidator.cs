using FluentValidation;
using SDCWebApp.Models.Dtos;

namespace SDCWebApp.Data.Validation
{
    public class SightseeingInfoDtoValidator : AbstractValidator<SightseeingInfoDto>, ICustomValidator<SightseeingInfoDto>
    {
        public SightseeingInfoDtoValidator()
        {
            RuleFor(x => x.Description)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(256);

            RuleFor(x => x.MaxAllowedGroupSize)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .InclusiveBetween(0, 100);

            RuleFor(x => x.MaxChildAge)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .InclusiveBetween(0, 18);

            RuleFor(x => x.MaxTicketOrderInterval)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .InclusiveBetween(0, 51);

            RuleFor(x => x.SightseeingDuration)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .InclusiveBetween(0, 24);
        }
    }
}
