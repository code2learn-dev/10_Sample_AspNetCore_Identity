using System.Text.RegularExpressions;

namespace Identity.ApplicationService.Account.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterDtoModel>
    {
        public RegisterValidator()
        {
            RuleFor(a => a.Email)
                .NotEmpty().WithMessage("ایمیل خود را وارد کنید")
                .EmailAddress().WithMessage("آدرس ایمیل معتبر وارد کنید");

            RuleFor(a => a.UserName)
                .NotEmpty().WithMessage("نام کاربری را وارد کنید")
                .Length(2, 50).WithMessage("نام کاربری باید بین 2 تا 50 حرف باشد");

            RuleFor(a => a.Password)
                .NotEmpty().WithMessage("رمز عبور را وارد کنید")
                .Length(2, 12).WithMessage("رمز عبور باید بین 2 تا 12 حرف باشد")
                .Must(a => Regex.IsMatch(a, @"^[a-zA-Z0-9!@#$%^&*()_+={}\[\];:'""<>/,.]{6,12}$")).WithMessage("رمز عبور باید شامل حروف [a-zA-Z] و حروف خاص  باشد");
        }
    }
}
