using FluentValidation;
using Identity.ApplicationService.Categories.Entites;

namespace Identity.ApplicationService.Categories.Validations
{
	public class CategoryValidator : AbstractValidator<CrudCategoryDto>
	{
		public CategoryValidator()
		{
			RuleFor(a => a.Title)
				.NotEmpty().WithMessage("عنوان دسته بندی را وارد کنید")
				.Length(2, 200).WithMessage("عنوان دسته بندی باید بین 2 تا 200 کاراکتر باشد");
		}
	}
}
