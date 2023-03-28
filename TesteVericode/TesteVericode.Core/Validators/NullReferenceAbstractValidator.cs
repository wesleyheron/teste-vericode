using FluentValidation;
using FluentValidation.Results;

namespace TesteVericode.Core.Validators
{
    public class NullReferenceAbstractValidator<T> : AbstractValidator<T>
    {
        public new ValidationResult Validate(T instance)
        {
            return instance == null
                ? new ValidationResult(new[] { new ValidationFailure(instance.ToString(), "response cannot be null", "Error") })
                : base.Validate(instance);
        }
    }
}
