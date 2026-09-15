using Identity.ApplicationService.Tokens.Entities;

namespace Identity.ApplicationService.Tokens.Validators
{
	public class CreateUserTokenValidator : AbstractValidator<CreateUserTokenDtoModel>
	{
		public CreateUserTokenValidator()
		{
			RuleFor(a => a.UserId)
				.NotEmpty().WithMessage("کاربری مشخص نشده است");

			RuleFor(a => a.Token)
				.NotEmpty().WithMessage("توکنی برای کاربر مشخص نشده است");
		}
	}
}
