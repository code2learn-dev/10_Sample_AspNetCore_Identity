using Microsoft.AspNetCore.Mvc.Rendering;

namespace Identity.WebAppRazorPage.ViewModels.Courses
{
    public class DeleteCourseViewModel : BaseViewModel
    {
		[Display(Name = "عنوان دوره آموزشی")] 
		public string Title { get; set; } = string.Empty;

		[Display(Name = "قیمت دوره آموزشی")] 
		public decimal Price { get; set; }

		[Display(Name = "دسته آموزشی")] 
		public long CategoryId { get; set; }

		[Display(Name = "تصویر دوره آموزشی")] 
		public string Image { get; set; } = string.Empty;

		[Display(Name = "نام مدرس")]
        public long TeacherId { get; set; }

        public IReadOnlyCollection<SelectListItem>? CategorySelectListItem { get; set; }

		public IReadOnlyCollection<SelectListItem>? TeacherSelectListItem { get; set; }

		public string CourseImageUrl { get; set; } = string.Empty;

    }
}
