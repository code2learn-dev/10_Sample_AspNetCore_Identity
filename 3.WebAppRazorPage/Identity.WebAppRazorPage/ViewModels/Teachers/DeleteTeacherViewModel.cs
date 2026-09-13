namespace Identity.WebAppRazorPage.ViewModels.Teachers
{
    public class DeleteTeacherViewModel : BaseViewModel
    {
		[Display(Name = "نام مدرس")]
		public string FirstName { get; set; } = string.Empty;
		
		[Display(Name = "نام خانوادگی مدرس")]
		public string LastName { get; set; } = string.Empty;
		
		[Display(Name = "کد ملی مدرس")]
		public decimal NationaCode { get; set; }
		
		public string Image { get; set; } = string.Empty;

		public string TeacherImageUrl { get; set; } = string.Empty;
    }
}
