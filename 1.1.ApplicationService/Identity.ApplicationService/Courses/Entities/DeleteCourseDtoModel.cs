using Identity.ApplicationService.Common;

namespace Identity.ApplicationService.Courses.Entities
{
    public class DeleteCourseDtoModel : BaseEntityDto
    {
		public string Title { get; set; } = string.Empty;

		public decimal Price { get; set; }

		public string Image { get; set; } = string.Empty;

		public long CategoryId { get; set; }

        public long TeacherId { get; set; }
    }
}
