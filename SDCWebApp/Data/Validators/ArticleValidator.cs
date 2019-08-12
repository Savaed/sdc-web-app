using FluentValidation;
using FluentValidation.Results;
using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Data.Validators
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
