namespace Identity.Domain.Courses
{
    public class CourseTag
    {
        public long CourseId { get; set; }
        public Course? Course { get; set; }

        public long TagId { get; set; }
        public Tag? Tag { get; set; }
    }
}
