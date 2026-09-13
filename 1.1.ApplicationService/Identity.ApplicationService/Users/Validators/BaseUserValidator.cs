using System.Text.RegularExpressions;

namespace Identity.ApplicationService.Users.Validators
{
    public class BaseUserValidator<TEntityDtoModel> : AbstractValidator<TEntityDtoModel>
        where TEntityDtoModel : BaseCrudUserDtoModel
    {
        public BaseUserValidator() {
            RuleFor(a => a.FirstName)
                .NotEmpty().WithMessage("نام کاربر را وارد کنید")
                .Length(2, 200).WithMessage("نام کاربر باید بین 2 تا 200 کاراکتر باشد");

			RuleFor(a => a.LastName)
			   .NotEmpty().WithMessage("نام خانوادگی کاربر را وارد کنید")
			   .Length(2, 200).WithMessage("نام خانوادگی کاربر باید بین 2 تا 200 کاراکتر باشد");

            RuleFor(a => a.Email)
                .NotEmpty().WithMessage("ایمیل کاربر را وارد کنید")
                .EmailAddress().WithMessage("آدرس ایمیل معتبری را وارد کنید");

            RuleFor(a => a.PhoneNumber)
                .NotEmpty().WithMessage("شماره تماس کاربر را وارد کنید")
                .Must(a => Regex.IsMatch(a, @"^09\d{9}$")).WithMessage("شماره تماس باید 11 رقمی باشد");

            RuleFor(a => a.RoleId)
                .NotEmpty().WithMessage("نقشی برای کاربر انتخاب نشده است");
		}
    }
}
