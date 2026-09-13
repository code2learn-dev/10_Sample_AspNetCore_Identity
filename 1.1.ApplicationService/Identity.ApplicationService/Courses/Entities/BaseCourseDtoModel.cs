using Identity.ApplicationService.Common;

namespace Identity.ApplicationService.Courses.Entities
{
    public class BaseCourseDtoModel : BaseEntityDto
    {
		public string Title { get; set; } = string.Empty;

		public decimal Price { get; set; } 

		public long CategoryId { get; set; }

		public long TeacherId { get; set; }

		public string Description { get; set; } = string.Empty;

	}
}
