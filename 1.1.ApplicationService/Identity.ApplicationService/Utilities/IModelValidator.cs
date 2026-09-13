using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.ApplicationService.Utilities
{
    public interface IModelValidator
    {
        Task<ValidationResult> ValidateModelAsync<TEntityModel>(TEntityModel model) where TEntityModel : BaseEntityDto;
    }

    public class ModelValidator : IModelValidator
    {
        private readonly IServiceProvider _serviceProvider;

        public ModelValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<ValidationResult> ValidateModelAsync<TEntityModel>(TEntityModel model)
			where TEntityModel : BaseEntityDto
		{
            IValidator<TEntityModel>? validator = _serviceProvider.GetService<IValidator<TEntityModel>>(); 

            return validator is not null
                    ? await validator.ValidateAsync(model)
                    : new ValidationResult();
        }
    }
}
