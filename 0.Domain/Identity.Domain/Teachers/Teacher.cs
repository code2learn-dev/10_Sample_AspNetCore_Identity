using Identity.Domain.Common;
using Identity.Domain.Courses;

namespace Identity.Domain.Teachers
{
    public class Teacher : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public decimal NationaCode { get; set; }
        public string Image { get; set; } = string.Empty;

        public ICollection<Course>? Courses { get; set; }

        public Degree? Degree { get; set; }
    }
}
