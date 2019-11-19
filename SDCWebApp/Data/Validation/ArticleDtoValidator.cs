using FluentValidation;
using SDCWebApp.Models.Dtos;

namespace SDCWebApp.Data.Validation
{
    public class ArticleDtoValidator : AbstractValidator<ArticleDto>, ICustomValidator<ArticleDto>
    {
        public ArticleDtoValidator()
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
