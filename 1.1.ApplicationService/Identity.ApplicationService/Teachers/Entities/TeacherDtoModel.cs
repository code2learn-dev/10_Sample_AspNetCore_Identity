namespace Identity.ApplicationService.Teachers.Entities
{
    public class TeacherDtoModel : BaseEntityDto
    {
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public decimal NationaCode { get; set; } 
	}
}
