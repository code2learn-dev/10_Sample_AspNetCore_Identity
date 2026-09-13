namespace Identity.ApplicationService.Courses.Validations
{
    public class CrudCourseValidators<TCourseDtoModel> : AbstractValidator<TCourseDtoModel>
		where TCourseDtoModel : BaseCourseDtoModel
    {
        public CrudCourseValidators() {
			RuleFor(a => a.Title)
			.NotEmpty()
			.WithMessage("لطفا عنوان دوره آموزشی را وارد کنید")
			.Length(2, 200)
			.WithMessage("عنوان دوره آموزشی باید بین 2 تا 200 کاراکتر باشد");

			RuleFor(a => a.Price)
				.InclusiveBetween(0, 50_000_000)
				.WithMessage("هزینه دوره آموزشی باید عددی بین 0 تا 50 میلیون باشد");

			RuleFor(a => a.CategoryId)
				.GreaterThan(0)
				.WithMessage("دسته آموزشی معتبری را انتخاب کنید");

			RuleFor(a => a.TeacherId)
				.GreaterThan(0)
				.WithMessage("مدرس دوره معتبری را انتخاب کنید");
		}
    }
}
