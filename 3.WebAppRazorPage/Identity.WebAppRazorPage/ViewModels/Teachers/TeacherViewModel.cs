namespace Identity.WebAppRazorPage.ViewModels.Teachers
{
    public class TeacherViewModel : BaseViewModel
    {
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public decimal NationaCode { get; set; }
	}
}
