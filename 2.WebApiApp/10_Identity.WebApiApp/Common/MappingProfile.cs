using Identity.ApplicationService.Categories.Entites;

namespace _10_Identity.WebApiApp.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryDto, CategoryViewModel>();
            CreateMap<CrudCategoryViewModel, CrudCategoryDto>(); 
        }
    }
}
