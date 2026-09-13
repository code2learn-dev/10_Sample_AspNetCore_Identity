using Identity.Domain.IDentityContent;

namespace Identity.WebAppRazorPage.Dependencies.Identities
{
    public class AcademyPasswordValidator : PasswordValidator<AcademyUser>
    {
        IReadOnlyList<String> _blockedPassword = ["123456", "12345678", "admin123456"];

        public override Task<IdentityResult> ValidateAsync(UserManager<AcademyUser> manager, AcademyUser user, string? password)
        {
            if(_blockedPassword.Contains(password))
            {
                var result = IdentityResult.Failed(new IdentityError()
                {
                    Code = "invalid password",
                    Description = "لطفا رمز عبور قوی تری را انتخاب کنید"
                });

                return Task.FromResult(result);
            }

            return base.ValidateAsync(manager, user, password);
        }
    }
}
