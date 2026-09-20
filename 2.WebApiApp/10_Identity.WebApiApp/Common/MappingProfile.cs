using _10_Identity.WebApiApp.ViewModels.Accounts;
using _10_Identity.WebApiApp.ViewModels.Categories;
using Identity.ApplicationService.Account.Entites;
using Identity.ApplicationService.Categories.Entites;

namespace _10_Identity.WebApiApp.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryDto, CategoryViewModel>();
            CreateMap<CrudCategoryViewModel, CrudCategoryDto>();


            // account mappings
            CreateMap<LoginViewModel, LoginDtoModel>();
        }
    }
}
