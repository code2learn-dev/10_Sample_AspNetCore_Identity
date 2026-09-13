using Identity.ApplicationService.Common;

namespace Identity.ApplicationService.Courses.Entities
{
    public class CourseDtoModel : BaseEntityDto
    {
		public string Title { get; set; } = string.Empty;

		public decimal Price { get; set; }

		public string Image { get; set; } = string.Empty;

	}
}
