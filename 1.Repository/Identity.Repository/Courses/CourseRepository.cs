using Identity.Domain.Courses;
using Identity.Domain.IDentityContent;
using Identity.Domain.Teachers;
using Identity.Repository.Common;
using Identity.Repository.Teachers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Identity.Repository.Courses
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly ITeacherRepository _teacherRepository;

        public CourseRepository(AcademyDbContext context, ITeacherRepository teacherRepository) : base(context)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<Course?> FindCourseForCrudAsync(long id) 
            => await _dbSet.Include(a => a.Teachers).FirstOrDefaultAsync(d => d.Id == id);

        public async Task<Course?> CreateAsync(Course model, long teacherId)
        {
            Teacher? teacher = _teacherRepository.FindById(teacherId);
            if (teacher is null) return default;

            model.Teachers.Add(teacher);
            Course? course = await AddAsync(model);

            return course is not null ? course : default;
        } 

        public async Task<Course?> UpdateAsync(Course model, long teacherId)
        {
            Course? course = await _dbSet.Include(a => a.Teachers).SingleOrDefaultAsync(d => d.Id == model.Id);
            if (course is null) return default;

            Teacher? teacher = _teacherRepository.FindById(teacherId);
            if (teacher is null) return default;

            // update course teachers
            // 1. remove current course teachers
            course.Teachers.Clear();
            // 2. add new teacher 
            course.Teachers.Add(teacher);

            // adding rest of the course properties
            course.Title = model.Title;
            course.Price = model.Price;
            course.Description = model.Description;
            course.Image = model.Image;
            course.CategoryId = model.CategoryId;

            Course? updatedCourse = await UpdateAsync(course);
            return updatedCourse is not null ? updatedCourse : default;
		}
    }
}
