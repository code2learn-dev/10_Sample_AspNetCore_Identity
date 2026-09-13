using Identity.ApplicationService.Claims.Entities;

namespace Identity.ApplicationService.Claims.Validators
{
	public interface IClaimValidator
	{
		Task<ApplicationServiceResult<ClaimDtoModel?>> ValidateClaimAsync(CrudClaimDtoModel model);
	}

	public class ClaimValidator : IClaimValidator
	{
		private readonly IServiceProvider _serviceProbider;

		public ClaimValidator(IServiceProvider serviceProbider)
		{
			_serviceProbider = serviceProbider;
		}

		public async Task<ApplicationServiceResult<ClaimDtoModel?>> ValidateClaimAsync(CrudClaimDtoModel model)
		{
			ApplicationServiceResult<ClaimDtoModel?> result = new();
			IValidator<CrudClaimDtoModel>? validator = _serviceProbider.GetService<IValidator<CrudClaimDtoModel>>();

			if (validator is not null)
			{
				ValidationResult validationResult = await validator.ValidateAsync(model);
				if (!validationResult.IsValid)
				{
					List<string> errors = validationResult.GetValidationResultErrors();
					result.AddErrorsList(errors.ToArray());
				}
			}
			else
			{
				result.AddError("خطا در اعتبارسنجی مدل ارسالی");
			}

			return result;
		}
	}
}
