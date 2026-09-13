namespace Identity.WebAppRazorPage.ViewModels.Categories
{
    public class CrudCategoryViewModel : BaseViewModel
    {
        [Display(Name = "عنوان دسته بندی")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "عنوان دسته بندی را وارد کنید")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "عنوان دسته آموزشی باید بین 2 تا 100 کاراکتر باشد")]
        public string Title { get; set; } = string.Empty;
    }
}
