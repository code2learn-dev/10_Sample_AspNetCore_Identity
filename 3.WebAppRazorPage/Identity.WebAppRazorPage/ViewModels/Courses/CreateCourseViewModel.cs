namespace Identity.WebAppRazorPage.ViewModels.Courses
{
    public class CreateCourseViewModel : BaseCourseViewModel
    {
        [Display(Name = "تصویر دوره آموزشی")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "تصویر دوره آموزشی را انتخاب کنید")]
        [Length(2, 200, ErrorMessage = "نام فایل تصویر دوره آموزشی باید بین 2 تا 200 کاراکتر باشد")]
        public string Image { get; set; } = string.Empty;

        [FileValidation(RequiredImageErrorMessage = "لطفا فایلی را برای تصویر دوره آموزشی انتخاب کنید")] 
        public IFormFile? File { get; set; }

    }
}
