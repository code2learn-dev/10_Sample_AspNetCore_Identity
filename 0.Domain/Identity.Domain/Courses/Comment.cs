using Identity.Domain.Common;

namespace Identity.Domain.Courses
{
    public class Comment : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;

        public long CourseId { get; set; } 
    }
}
