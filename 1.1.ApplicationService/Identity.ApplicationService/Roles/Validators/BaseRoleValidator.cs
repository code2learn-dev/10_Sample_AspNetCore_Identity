namespace Identity.ApplicationService.Roles.Validators
{
    public abstract class BaseRoleValidator<TRoleEntityModel> : AbstractValidator<TRoleEntityModel>
            where TRoleEntityModel : BaseCrudRoleDtoModel
    {
        public BaseRoleValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("نام نقش را وارد کنید")
                .Length(2, 50).WithMessage("نام نقش باید بین 2 تا 60 حرف باشد");

            RuleFor(a => a.Description)
                .NotEmpty().WithMessage("توضیحات نقش را وارد کنید")
                .Length(10, 500).WithMessage("توضیحات نقش وارد شده باید بین 20 تا 500 حرف باشد");
        }
    }
}
