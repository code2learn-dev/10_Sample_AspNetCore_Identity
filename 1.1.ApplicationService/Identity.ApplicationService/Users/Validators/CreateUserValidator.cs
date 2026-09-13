using System.Text.RegularExpressions;

namespace Identity.ApplicationService.Users.Validators
{
	public class CreateUserValidator : BaseUserValidator<CreateUserDtoModel>
	{
		public CreateUserValidator()
		{
			RuleFor(a => a.Password)
				.NotEmpty().WithMessage("رمز عبور را وارد کنید")
				.Length(6, 12).WithMessage("رمز عبور باید بین 2 تا 12 کاراکتر باشد")
				.Must(a => Regex.IsMatch(a, @"^[A-Za-z\d!@#$%^&*(){}|:<>;?\/]{6,12}$"))
								.WithMessage("فقط امکان استفاده از حروف a-zA-Z0-9 و علائم وجود دارد");
		}
	}
}
