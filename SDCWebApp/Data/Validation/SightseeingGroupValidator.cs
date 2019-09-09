using FluentValidation;
using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Data.Validation
{
    public class SightseeingGroupValidator : AbstractValidator<SightseeingGroup>, ICustomValidator<SightseeingGroup>
    {
        public SightseeingGroupValidator(ApplicationDbContext dbContext)
        {
            var recentInfo = dbContext.GeneralSightseeingInfo.OrderByDescending(x => x.UpdatedAt == null ? x.CreatedAt : x.UpdatedAt).First();

            RuleFor(x => x.MaxGroupSize)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .InclusiveBetween(0, 40);

            RuleFor(x => x.CurrentGroupSize)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .InclusiveBetween(0, 40);

            RuleFor(x => x.SightseeingDate)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .InclusiveBetween(DateTime.Now, DateTime.Now.AddDays(recentInfo.MaxTicketOrderInterval * 7))    // MaxTicketOrderInterval is in weeks.
                .Custom((time, context) =>
                {
                    if (time.Minute != 0)
                    {
                        context.AddFailure($"An hour of {nameof(SightseeingGroup.SightseeingDate)} must be full.");
                    }
                   
                    int openingHour = recentInfo.OpeningDateTime.Hour;
                    int openingMinute = recentInfo.OpeningDateTime.Minute;

                    int closingHour = recentInfo.ClosingDateTime.Hour;
                    int closingMinute = recentInfo.ClosingDateTime.Minute;

                    if ((time.Hour < openingHour && time.Minute < openingMinute) || (time.Hour > closingHour && time.Minute > closingMinute))
                    {
                        context.AddFailure($"{nameof(SightseeingGroup.SightseeingDate)} must be in company opening hour which is " +
                            $"'{openingHour}:{openingMinute} - {closingHour}:{closingMinute}'");
                    }
                });
        }
    }
}
