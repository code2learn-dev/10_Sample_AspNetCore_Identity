namespace Identity.WebAppRazorPage.ViewModels.Teachers
{
    public class CrudTeacherViewModel : BaseViewModel
    {
		[Display(Name = "نام مدرس")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "نام مدرس را وارد کنید")]
		[StringLength(200, MinimumLength = 2, ErrorMessage = "نام مدرس باید بین 2 تا 200 کاراکتر باشد")]
		public string FirstName { get; set; } = string.Empty;

		[Display(Name = "نام خانوادگی مدرس")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "نام خانوادگی مدرس را وارد کنید")]
		[StringLength(200, MinimumLength = 2, ErrorMessage = "نام خانوادگی مدرس باید بین 2 تا 200 کاراکتر باشد")]
		public string LastName { get; set; } = string.Empty;

		[Display(Name = "کد ملی مدرس")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "کد ملی مدرس را وارد کنید")]
		[RegularExpression(@"^\d{10}$", ErrorMessage = "کد ملی باید 10 رقم باشد")]
		[DisplayFormat(DataFormatString = "{0:F0}", ApplyFormatInEditMode = true)]
		public decimal NationaCode { get; set; }

		public string TeacherImageUrl { get; set; } = string.Empty;
    }
}
