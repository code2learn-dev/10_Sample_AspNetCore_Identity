using Microsoft.AspNetCore.Mvc.Rendering;

namespace Identity.WebAppRazorPage.ViewModels.Users
{
    public class BaseCrudUserViewModel
    {
		public Guid Id { get; set; }

		[Display(Name = "نام")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "نام کاربر را وارد کنید")]
		[StringLength(200, MinimumLength = 2, ErrorMessage = "نام کاربر باید بین 2 تا 200 حرف باشد")]
		public string FirstName { get; set; } = string.Empty;

		[Display(Name = "نام خانوادگی")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "نام خانوادگی کاربر را وارد کنید")]
		[StringLength(200, MinimumLength = 2, ErrorMessage = "نام خانوادگی کاربر باید بین 2 تا 200 حرف باشد")] 
		public string LastName { get; set; } = string.Empty;

		[Display(Name = "نام کاربری")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "نام کاربری را وارد کنید")]
		[StringLength(200, MinimumLength = 2, ErrorMessage = "نام کاربری باید بین 2 تا 200 حرف باشد")]
		public string UserName { get; set; } = string.Empty;

		[Display(Name = "آدرس ایمیل")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "آدرس ایمیل کاربر را وارد کنید")]
		[DataType(DataType.EmailAddress, ErrorMessage = "آدرس ایمیل معتبر برای کاربر وارد کنید")]
		[EmailAddress(ErrorMessage = "آدرس ایمیل معتبری را برای کاربر وارد کنید")]
		public string Email { get; set; } = string.Empty;

		[Display(Name = "شماره تماس")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "شماره تماس را وارد کنید")]
		[RegularExpression(@"^09\d{9}$", ErrorMessage = "شماره تماس باید 11 رقمی باشد")]
		public string PhoneNumber { get; set; } = string.Empty;
		 
		public bool PhoneNumberConfirmed { get; set; }

		public bool EmailConfirmed { get; set; }

		[Display(Name = "ورود دو مرحله ای")]
		public bool TwoFactorEnabled { get; set; }

		public string? Image { get; set; } = string.Empty;

		[FileValidation(IsRequired = false)]
        public IFormFile? File { get; set; }

		[Display(Name = "نقش کاربر")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "نقش کاربر را انتخاب کنید")] 
		public string RoleId { get; set; } = string.Empty;

        public IReadOnlyCollection<SelectListItem>? RoleSelectListItem { get; set; }

    }
}
