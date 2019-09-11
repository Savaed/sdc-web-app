using FluentValidation;
using SDCWebApp.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
