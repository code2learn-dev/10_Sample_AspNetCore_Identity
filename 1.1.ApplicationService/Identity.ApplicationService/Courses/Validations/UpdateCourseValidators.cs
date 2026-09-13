namespace Identity.ApplicationService.Courses.Validations
{
    public class UpdateCourseValidators : CrudCourseValidators<UpdateCourseDtoModel>
    {
        public UpdateCourseValidators() {
            RuleFor(a => a.Image)
                .Length(2, 200).WithMessage("تصویر فایل دوره آموزشی باید بین 2 تا 200 کاراکتر باشد");
        }
    }
}
