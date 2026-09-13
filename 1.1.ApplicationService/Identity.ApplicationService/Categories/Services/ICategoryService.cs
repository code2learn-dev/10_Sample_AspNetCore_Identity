using Identity.ApplicationService.Categories.Entites;
using Identity.ApplicationService.Contracts;
using Identity.Domain.Categories;

namespace Identity.ApplicationService.Categories.Services
{
    public interface ICategoryService 
        : IBaseDtoService<
            CategoryService,
            Category,
            CategoryDto,
            CrudCategoryDto,
            CrudCategoryDto,
            CategoryDto>
    {
    }
}
