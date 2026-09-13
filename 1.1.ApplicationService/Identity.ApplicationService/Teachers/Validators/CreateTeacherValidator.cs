namespace Identity.ApplicationService.Teachers.Validators
{
    public class CreateTeacherValidator : CrudTeacherValidator<CreateTeacherDtoModel>
    {
        public CreateTeacherValidator() {
            RuleFor(a => a.Image)
                .NotEmpty().WithMessage("تصویر مدرس را انتخاب کنید");
        }
    }
}
