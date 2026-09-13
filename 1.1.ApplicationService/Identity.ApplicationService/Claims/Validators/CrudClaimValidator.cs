using Identity.ApplicationService.Claims.Entities;

namespace Identity.ApplicationService.Claims.Validators
{
    public class CrudClaimValidator : AbstractValidator<CrudClaimDtoModel>
    {
        public CrudClaimValidator()
        {
            RuleFor(a => a.ClaimType).NotEmpty().WithMessage("نوع claim را مشخص کنید"); 
            RuleFor(a => a.ClaimValue).NotEmpty().WithMessage("مقدار claim را وارد کنید");  
        }
    }
}
