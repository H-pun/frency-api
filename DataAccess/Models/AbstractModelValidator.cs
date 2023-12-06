using FluentValidation;

namespace Frency.DataAccess.Models
{
    public abstract class AbstractModelValidator<T> : AbstractValidator<T> where T : class { }
}
