namespace Identity.ApplicationService.Roles.Validators
{
    public interface IRoleModelValidator 
    {
        Task<ValidationResult> ValidateModelAsync<TRoleEntityModel>(TRoleEntityModel model)
                                where TRoleEntityModel : BaseCrudRoleDtoModel;
    }

    public class RoleModelValidator : IRoleModelValidator
    {
        private readonly IServiceProvider _serviceProvider;

        public RoleModelValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<FluentValidation.Results.ValidationResult> 
            ValidateModelAsync<TRoleEntityModel>(TRoleEntityModel model)
            where TRoleEntityModel : BaseCrudRoleDtoModel
        {
            IValidator<TRoleEntityModel>? validator = _serviceProvider.GetService<IValidator<TRoleEntityModel>>();
            return validator is not null
                    ? await validator.ValidateAsync(model)
                    : new();
        }
    }
}
