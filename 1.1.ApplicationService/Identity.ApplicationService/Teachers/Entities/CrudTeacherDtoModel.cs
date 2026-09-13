namespace Identity.ApplicationService.Teachers.Entities
{
    public class CrudTeacherDtoModel : BaseEntityDto
    {
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public decimal NationaCode { get; set; }
		public string Image { get; set; } = string.Empty;   
	}
}
