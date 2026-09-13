namespace Identity.ApplicationService.Account.Validators
{
    public interface IAccountModelValidator
    {
        Task<ValidationResult> ValidateModelAsync<TAccountModel>(TAccountModel model);
    }

    public class AccountModelValidator : IAccountModelValidator
    {
        private readonly IServiceProvider _serviceProvider;

        public AccountModelValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<ValidationResult> ValidateModelAsync<TAccountModel>(TAccountModel model)
        {
            var validator = _serviceProvider.GetService<IValidator<TAccountModel>>();
            return validator is not null
                    ? await validator.ValidateAsync(model)
                    : new ValidationResult();
        }
    }
}
