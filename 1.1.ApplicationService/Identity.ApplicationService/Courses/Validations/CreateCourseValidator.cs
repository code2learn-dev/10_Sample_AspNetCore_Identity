namespace Identity.ApplicationService.Courses.Validations
{
	public class CreateCourseValidator : CrudCourseValidators<CreateCourseDtoModel>
	{
		public CreateCourseValidator()
		{
			RuleFor(a => a.Image)
				.NotEmpty().WithMessage("تصویر دوره آموزشی را انتخاب کنید")
				.Length(2, 200).WithMessage("نام فایل انتخابی نامعتبر می باشد");
		}
	}
}
