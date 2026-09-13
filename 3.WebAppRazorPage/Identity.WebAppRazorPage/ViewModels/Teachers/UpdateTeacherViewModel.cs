namespace Identity.WebAppRazorPage.ViewModels.Teachers
{
    public class UpdateTeacherViewModel : CrudTeacherViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "تصویری برای مدرس انتخاب نشده است")]
        public string Image { get; set; } = string.Empty;

        [FileValidation(IsRequired = false)]
        public IFormFile? File { get; set; }
    }
}
