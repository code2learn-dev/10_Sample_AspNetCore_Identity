namespace Identity.WebAppRazorPage.ViewModels.Common
{
	public class FileValidationAttribute : ValidationAttribute
	{
		const int file_size = 5 * (1024 * 1024);
		string[] allowd_types = ["image/jpg", "image/jpeg", "image/png"];

		public bool IsRequired { get; set; } = true;
		
		public string RequiredImageErrorMessage { get; set; } = "فایلی برای تصویر انتخاب نشده است";
		 

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value is null && IsRequired) return new ValidationResult(RequiredImageErrorMessage);
			else if (value is null && !IsRequired) return ValidationResult.Success;

			IFormFile? file = value as IFormFile;

			if (file is not null)
			{
				if (file.Length > 0)
				{
					if (file.Length > file_size)
						return new ValidationResult("اندازه فایل انتخابی بیش از 5 مگابایت می باشد");

					if (!allowd_types.Contains(file.ContentType))
						return new ValidationResult("نوع فایل انتخابی نامعتبر می باشد");
					
					return ValidationResult.Success;
				}
				else if (file.Length == 0 && IsRequired)
				{
					return new ValidationResult(RequiredImageErrorMessage);
				}
				else if (file.Length == 0 && !IsRequired)
				{
					return ValidationResult.Success;
				}

			}

			return ValidationResult.Success;
		}
	}
}
