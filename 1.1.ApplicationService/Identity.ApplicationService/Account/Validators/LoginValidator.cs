namespace Identity.ApplicationService.Account.Validators
{
    public class LoginValidator : AbstractValidator<LoginDtoModel>
    {
        public LoginValidator() {
            RuleFor(a => a.UserName)
                .NotEmpty().WithMessage("نام کاربری را وارد کنید");

            RuleFor(a => a.Password)
                .NotEmpty().WithMessage("رمز عبور را وارد کنید");
        }
    }
}
