using Identity.WebAppRazorPage.ViewModels.Common;

namespace Identity.WebAppRazorPage.ViewModels.Courses
{
    public class CourseViewModel : BaseViewModel
    {
		public string Title { get; set; } = string.Empty;

		public decimal Price { get; set; }

		public string Image { get; set; } = string.Empty;
	}
}
