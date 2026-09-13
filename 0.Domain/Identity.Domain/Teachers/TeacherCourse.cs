using Identity.Domain.Courses;

namespace Identity.Domain.Teachers
{
    public class TeacherCourse
    {
        public long CourseId { get; set; }
        public Course? Course { get; set; }

        public long TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
    }
}
