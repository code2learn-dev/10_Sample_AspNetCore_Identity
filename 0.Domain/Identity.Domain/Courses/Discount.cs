using Identity.Domain.Common;

namespace Identity.Domain.Courses
{
    public class Discount : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public decimal NewPrice { get; set; }

        public long CourseId { get; set; }
    }
}
