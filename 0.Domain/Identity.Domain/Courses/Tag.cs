using Identity.Domain.Common;

namespace Identity.Domain.Courses
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Course>? Courses { get; set; }
    }
}
