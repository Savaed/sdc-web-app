using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SDCWebApp.Models;
using System;
using System.Linq;

namespace SDCWebApp.Data.Validation
{
    public class SightseeingGroupValidator : AbstractValidator<SightseeingGroup>, ICustomValidator<SightseeingGroup>
    {
        public SightseeingGroupValidator(ApplicationDbContext dbContext)
        {
            var recentInfo = dbContext.Info.Include(x => x.OpeningHours).OrderByDescending(x => x.UpdatedAt == DateTime.MinValue ? x.CreatedAt : x.UpdatedAt).First();

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
                        context.AddFailure($"An hour of {nameof(SightseeingGroup.SightseeingDate)} must be full.");
                    }

                    var closingDateTime = recentInfo.GetClosingDateTime(dateTime);
                    var openingDateTime = recentInfo.GetOpeningDateTime(dateTime);

                    if (dateTime < openingDateTime || dateTime > closingDateTime)
                    {
                        context.AddFailure($"{nameof(SightseeingGroup.SightseeingDate)} must be in company opening hour which is " +
                            $"'{openingDateTime.Hour.ToString()} - {closingDateTime.Hour.ToString()}'");
                    }
                });
        }
    }
}
