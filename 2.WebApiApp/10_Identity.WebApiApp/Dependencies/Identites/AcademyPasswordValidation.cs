
namespace _10_Identity.WebApiApp.Dependencies.Identites
{
	public class AcademyPasswordValidation : PasswordValidator<AcademyUser>
	{
		private IReadOnlyCollection<string> _blockedPassword = ["123456", "admin", "admin1234", "12345"];

		public override Task<IdentityResult> ValidateAsync(UserManager<AcademyUser> manager, AcademyUser user, string? password)
		{
			if (_blockedPassword.Contains(password))
			{
				var result = IdentityResult.Failed(
					new IdentityError()
					{
						Code = "password too weak",
						Description = "رمز عبور قوی تری انتخاب کنید"
					});

				return Task.FromResult(result);
			}

			return base.ValidateAsync(manager, user, password);
		}
	}
}
