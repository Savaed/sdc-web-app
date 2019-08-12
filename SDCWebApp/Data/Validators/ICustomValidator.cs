using FluentValidation.Results;

namespace SDCWebApp.Data.Validators
{
    public interface ICustomValidator<T> where T : class
    {
        ValidationResult Validate(T instance);
    }
}