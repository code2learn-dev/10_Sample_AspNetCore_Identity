namespace Identity.ApplicationService.Users.Validators
{
    public interface IUserModelValidator
    {
        Task<ValidationResult> 
            ValidateModelAsync<TUserDtoModel>(TUserDtoModel model) where TUserDtoModel : BaseCrudUserDtoModel;

        Task<ValidationResult> ValidatePasswordAsync(ChangePasswordDtoModel model);
    }

    public class UserModelValidator : IUserModelValidator
    {
        private readonly IServiceProvider _serviceProvider;

        public UserModelValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<ValidationResult> 
            ValidateModelAsync<TUserDtoModel>(TUserDtoModel model) where TUserDtoModel : BaseCrudUserDtoModel
        {
            IValidator<TUserDtoModel>? validator = _serviceProvider.GetService<IValidator<TUserDtoModel>>();
            return validator is not null
                    ? await validator.ValidateAsync(model)
                    : new ValidationResult();
        }

        public async Task<ValidationResult> ValidatePasswordAsync(ChangePasswordDtoModel model)
        {
            IValidator<ChangePasswordDtoModel>? validator = _serviceProvider.GetService<IValidator<ChangePasswordDtoModel>>();
            return validator is not null
                ? await validator.ValidateAsync(model)
                : new ValidationResult();
        }
    } 
}
