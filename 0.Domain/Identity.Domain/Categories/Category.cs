using Identity.Domain.Common;
using Identity.Domain.Courses;

namespace Identity.Domain.Categories
{
    public class Category : BaseEntity
    {
        public string Title { get; set; } = string.Empty;

        public ICollection<Course>? Courses { get; set; }
    }
}
