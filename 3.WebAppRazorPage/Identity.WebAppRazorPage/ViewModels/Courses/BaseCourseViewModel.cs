using Microsoft.AspNetCore.Mvc.Rendering;

namespace Identity.WebAppRazorPage.ViewModels.Courses
{
    public class BaseCourseViewModel : BaseViewModel
    {
		[Display(Name = "عنوان دوره آموزشی")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "عنوان دوره آموزشی را وارد کنید")]
		[StringLength(200, MinimumLength = 2, ErrorMessage = "عنوان دوره آموزشی باید بین 2 تا 200 کاراکتر باشد")]
		public string Title { get; set; } = string.Empty;

		[Display(Name = "قیمت دوره آموزشی")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "هزینه دوره آموزشی را وارد کنید")]
		[Range(0, 50_000_000, ErrorMessage = "هزینه دوره آموزشی باید عددی بین 0 تا 50 میلیون باشد")]
		[DisplayFormat(DataFormatString = "{0:N0}")] 
		public decimal Price { get; set; }

		[Display(Name = "دسته آموزشی")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "دسته آموزشی را انتخاب کنید")]
		[RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "دسته آموزشی را درست انتخاب کنید")]
		public long CategoryId { get; set; }

		[Display(Name = "مدرس دوره")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "مدرس دوره را انتخاب کنید")]
		[RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "مدرس دوره را صحیح انتخاب کنید")]
		public long TeacherId { get; set; }

		public IReadOnlyCollection<SelectListItem>? CategorySelectListItem { get; set; }

        public IReadOnlyCollection<SelectListItem>? TeacherSelectListItem { get; set; }
    }
}
