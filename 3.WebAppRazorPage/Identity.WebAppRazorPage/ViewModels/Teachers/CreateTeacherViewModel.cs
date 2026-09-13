namespace Identity.WebAppRazorPage.ViewModels.Teachers
{
    public class CreateTeacherViewModel : CrudTeacherViewModel
    { 
        public string? Image { get; set; }

        [FileValidation(RequiredImageErrorMessage = "لطفا فایلی را برای تصویر مدرس انتخاب کنید")]
        public IFormFile? File { get; set; }
    }
}
