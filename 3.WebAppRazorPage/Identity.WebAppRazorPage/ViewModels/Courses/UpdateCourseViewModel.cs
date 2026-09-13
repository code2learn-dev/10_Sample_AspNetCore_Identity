namespace Identity.WebAppRazorPage.ViewModels.Courses
{
    public class UpdateCourseViewModel : BaseCourseViewModel
    {
		[Display(Name = "تصویر دوره آموزشی")] 
		[Length(2, 200, ErrorMessage = "نام فایل تصویر دوره آموزشی باید بین 2 تا 200 کاراکتر باشد")]
		public string Image { get; set; } = string.Empty;

		[FileValidation(IsRequired = false)]
        public IFormFile? File { get; set; }

        public string CourseImageUrl { get; set; } = string.Empty;
    }
}
