using Identity.Domain.Categories;
using Identity.Domain.IDentityContent;
using Identity.Repository.Common;

namespace Identity.Repository.Categories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AcademyDbContext context) : base(context)
        {
        }
    }
}
