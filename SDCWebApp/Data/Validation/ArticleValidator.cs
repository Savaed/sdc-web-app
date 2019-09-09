using FluentValidation;
using SDCWebApp.Models;

namespace SDCWebApp.Data.Validation
{
    public class ArticleValidator : AbstractValidator<Article>
    {
        public ArticleValidator()
        {
            RuleFor(x => x.Author)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Title)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Text)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty();
        }
    }
}
