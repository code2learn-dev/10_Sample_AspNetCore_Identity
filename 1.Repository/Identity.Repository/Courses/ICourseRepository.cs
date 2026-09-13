using Identity.Domain.Courses;
using Identity.Domain.Teachers;
using Identity.Repository.Common;

namespace Identity.Repository.Courses
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task<Course?> CreateAsync(Course model, long teacherId);
        Task<Course?> FindCourseForCrudAsync(long id);
        Task<Course?> UpdateAsync(Course model, long teacherId); 
    }
}
