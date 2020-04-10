namespace Domain.Validation.Abstractions
{
    public interface IValidator<in TModel>
    {
        void Validate(TModel model);
    }
}