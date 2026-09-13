namespace Identity.WebAppRazorPage.ViewModels.Roles
{
    public abstract class BaseCrudRoleViewModel
    {
		public string Id { get; set; } = string.Empty;

		[Display(Name = "عنوان نقش")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "عنوان نقش را وارد کنید")]
		[StringLength(50, MinimumLength = 2, ErrorMessage = "عنوان نقش باید بین 2 تا 50 حرف باشد")]
		public string Name { get; set; } = string.Empty;

		[Display(Name = "توضیحات نقش")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "توضیحات نقش را وارد کنید")]
		[StringLength(500, MinimumLength = 10, ErrorMessage = "توضیحات نقش باید بین 10 تا 500 حرف باشد")] 
		public string Description { get; set; } = string.Empty;
	}
}
