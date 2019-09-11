using FluentValidation;
using SDCWebApp.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Data.Validation
{
    public class SightseeingGroupDtoValidator : AbstractValidator<SightseeingGroupDto>, ICustomValidator<SightseeingGroupDto>
    {
        public SightseeingGroupDtoValidator(ApplicationDbContext dbContext)
        {
            var recentInfo = dbContext.GeneralSightseeingInfo.OrderByDescending(x => x.UpdatedAt == null ? x.CreatedAt : x.UpdatedAt).First();

            RuleFor(x => x.MaxGroupSize)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .InclusiveBetween(0, recentInfo.MaxAllowedGroupSize);

            RuleFor(x => x.CurrentGroupSize)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .InclusiveBetween(0, recentInfo.MaxAllowedGroupSize);

            RuleFor(x => x.SightseeingDate)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .GreaterThan(DateTime.Now)
                .LessThan(DateTime.Now.AddDays(recentInfo.MaxTicketOrderInterval * 7))
                .Custom((dateTime, context) =>
                {
                    if (dateTime.Minute != 0)
                    {
                        context.AddFailure($"An hour of {nameof(SightseeingGroupDto.SightseeingDate)} must be full.");
                    }

                    var closingDateTime = recentInfo.GetClosingDateTime(dateTime);
                    var openingDateTime = recentInfo.GetOpeningDateTime(dateTime);

                    if (dateTime < openingDateTime || dateTime > closingDateTime)
                    {
                        context.AddFailure($"{nameof(SightseeingGroupDto.SightseeingDate)} must be in company opening hour which is " +
                            $"'{openingDateTime.Hour.ToString()} - {closingDateTime.Hour.ToString()}'");
                    }
                });
        }
    }
}
