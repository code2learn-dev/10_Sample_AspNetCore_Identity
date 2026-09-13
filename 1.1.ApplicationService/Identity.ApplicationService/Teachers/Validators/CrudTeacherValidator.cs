using System.Text.RegularExpressions;

namespace Identity.ApplicationService.Teachers.Validators
{
	public abstract class CrudTeacherValidator<TTeacherDtoModel> : AbstractValidator<TTeacherDtoModel>
		where TTeacherDtoModel : CrudTeacherDtoModel
	{
		public CrudTeacherValidator()
		{
			RuleFor(a => a.FirstName)
				.NotEmpty().WithMessage("نام مدرس را وارد کنید")
				.Length(2, 200).WithMessage("نام مدرس باید بین 2 تا 200 کاراکتر باشد");

			RuleFor(a => a.LastName)
				.NotEmpty().WithMessage("نام مدرس را وارد کنید")
				.Length(2, 200).WithMessage("نام مدرس باید بین 2 تا 200 کاراکتر باشد");

			RuleFor(a => a.NationaCode)
				.NotEmpty().WithMessage("کد ملی مدرس را وارد کنید")
				.Must(d => Regex.IsMatch(d.ToString(), @"^\d{10}$")).WithMessage("کد ملی مدرس باید 10 رقم باشد");
		}
	}
}
