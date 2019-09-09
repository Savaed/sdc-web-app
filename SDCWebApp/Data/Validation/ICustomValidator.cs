using FluentValidation.Results;

namespace SDCWebApp.Data.Validation
{
    public interface ICustomValidator<T> where T : class
    {
        ValidationResult Validate(T instance);
    }
}