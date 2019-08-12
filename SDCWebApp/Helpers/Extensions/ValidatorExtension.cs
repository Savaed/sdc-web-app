using FluentValidation;
using System;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace SDCWebApp.Data.Validators
{
    public static class ValidatorExtensions
    {
        /// <summary>
        /// Indicates whether email address is in valid format.
        /// Valid email adresses acording https://en.wikipedia.org/wiki/Email_address#Examples
        /// </summary>
        public static IRuleBuilderOptions<T, string> BeEmailAddress<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(emailAddress =>
            {
                string localPart = emailAddress.Split("@")[0];

                // Local part of email (part before @) cannot be longer than 64 characters.
                // MailAddress class doesnt check this condition.
                if (localPart.Length > 64)
                    return false;
                try
                {
                    var email = new MailAddress(emailAddress);
                }
                catch (FormatException)
                {
                    return false;
                }
                return true;
            });
        }

        /// <summary>
        /// Indicates whether password is valid. Password must contain at least 8 characters including a number, unique symbol (#$% etc.) and a uppercase and lowercase letter.
        /// </summary>
        public static IRuleBuilderOptions<T, string> BeValidPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(password => new Regex(@"^(?=.*[A - Z])(?=.*[a - z])(?=.*\d)(?=.*[!#$%&'()*+,\-.\/:;<=>?@[\\\]^_`{|}~])(?=.{8,})").IsMatch(password));
        }

        /// <summary>
        /// Indicates whether string is a valid guid. Note that nill GUID is invalid in this case.
        /// </summary>
        public static IRuleBuilderOptions<T, string> BeGuid<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Custom((id, context) =>
            {
                if (id.Length > 38)
                    context.AddFailure("Incorrect length.");

                id = id.ToLower();

                if (id.First() == '{' && id.Last() == '}')
                {
                    id = id.Remove(0, 1);
                    id = id.Remove(id.Length - 1, 1);

                    if (id.Contains('{') || id.Contains('}'))
                        context.AddFailure("Symbol '{' is only allowed as first symbol of GUID and '}' as last symbol.");
                }

                if (id.Count(x => x == '-') == 4)
                {
                    if (id.Length != 36)
                        context.AddFailure("Incorrect length.");
                    var regex = new Regex(@"^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$");
                    if (!regex.IsMatch(id))
                        context.AddFailure("Incorrect format.");
                }
                else if (!id.Contains('-'))
                {
                    if (id.Length != 32)
                        context.AddFailure("Incorrect length.");
                    var regex = new Regex(@"^[0-9a-f]{32}$");
                    if (!regex.IsMatch(id))
                        context.AddFailure("Incorrect format.");
                }
                else
                    context.AddFailure("GUID can contain 4 or 0 '-' characters.");

                id = id.Replace("-", "");
                if (int.TryParse(id, out int result) && result == 0)
                    context.AddFailure("Cannot be nil GUID.");
            }) as IRuleBuilderOptions<T, string>;
        }
    }
}
