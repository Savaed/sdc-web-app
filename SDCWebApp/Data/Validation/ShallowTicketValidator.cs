using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SDCWebApp.Models.Dtos;
using System;
using System.Linq;

namespace SDCWebApp.Data.Validation
{
    public class ShallowTicketValidator : AbstractValidator<ShallowTicket>, ICustomValidator<ShallowTicket>
    {
        public ShallowTicketValidator(ApplicationDbContext dbContext)
        {
            var recentInfo = dbContext.GeneralSightseeingInfo.Include(x => x.OpeningHours).OrderByDescending(x => x.UpdatedAt == DateTime.MinValue ? x.CreatedAt : x.UpdatedAt).First();

            RuleFor(x => x.SightseeingDate)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .NotEmpty()
               .GreaterThan(DateTime.Now)
               .LessThan(DateTime.Now.AddDays(recentInfo.MaxTicketOrderInterval * 7))
               .Custom((dateTime, context) =>
               {
                   if (dateTime.Minute != 0)
                   {
                       context.AddFailure($"An hour of {nameof(ShallowTicket.SightseeingDate)} must be full.");
                   }

                   var closingDateTime = recentInfo.GetClosingDateTime(dateTime);
                   var openingDateTime = recentInfo.GetOpeningDateTime(dateTime);

                   if (dateTime < openingDateTime || dateTime > closingDateTime)
                   {
                       context.AddFailure($"{nameof(ShallowTicket.SightseeingDate)} must be in company opening hour which is " +
                           $"'{openingDateTime.Hour.ToString()} - {closingDateTime.Hour.ToString()}'");
                   }
               });
        }
    }
}
