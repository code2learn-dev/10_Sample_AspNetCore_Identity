using Identity.Domain.IDentityContent;
using Identity.Domain.Teachers;
using Identity.Repository.Common;

namespace Identity.Repository.Teachers
{
    public class TeacherRepository
        : GenericRepository<Teacher>,
          ITeacherRepository
    {
        public TeacherRepository(AcademyDbContext context) : base(context)
        {
        }
    }
}
