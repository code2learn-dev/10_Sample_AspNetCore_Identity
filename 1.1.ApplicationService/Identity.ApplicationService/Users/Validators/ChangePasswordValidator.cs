using System.Text.RegularExpressions;

namespace Identity.ApplicationService.Users.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDtoModel>
    {
        public ChangePasswordValidator() { 

            RuleFor(a => a.NewPassword)
                .NotEmpty().WithMessage("رمز عبور جدید وارد نشده است")
                .Length(6, 12).WithMessage("رمز عبور جاری باید بین 6 تا 12 حرف باشد")
                .Must(a => Regex.IsMatch(a, @"^[a-zA-Z\d!@#$%^&*+()_={}\[\].,;:'""/<>]{6,12}$")).WithMessage("رمز عبور باید از حروف [a-zA-Z0-9] و حروف خاص تشکیل شده باشد");
        }
    }
}
