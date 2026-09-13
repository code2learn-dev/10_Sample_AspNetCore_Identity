using Identity.ApplicationService.Account.Entites;
using Identity.WebAppRazorPage.ViewModels.Account;

namespace Identity.WebAppRazorPage.Common
{
    public class ViewModelMappingProfiles : Profile
    {
        public ViewModelMappingProfiles() {
            CreateMap<CategoryDto, CategoryViewModel>();
            CreateMap<CrudCategoryViewModel, CrudCategoryDto>();
            CreateMap<CrudCategoryDto, CrudCategoryViewModel>();


            CreateMap<CourseDtoModel, CourseViewModel>();
            CreateMap<CreateCourseViewModel, CreateCourseDtoModel>();
            CreateMap<UpdateCourseDtoModel, UpdateCourseViewModel>();
            CreateMap<UpdateCourseViewModel, UpdateCourseDtoModel>();
            CreateMap<DeleteCourseDtoModel, DeleteCourseViewModel>();

          
            CreateMap<TeacherDtoModel, TeacherViewModel>();
            CreateMap<CreateTeacherViewModel, CreateTeacherDtoModel>();
            CreateMap<UpdateTeacherDtoModel, UpdateTeacherViewModel>();
            CreateMap<UpdateTeacherViewModel, UpdateTeacherDtoModel>();
            CreateMap<DeleteTeacherDtoModel, DeleteTeacherViewModel>();


            // users mappings
            CreateMap<UserDtoModel, UserViewModel>();
            CreateMap<CreateUserViewModel, CreateUserDtoModel>();
            CreateMap<UpdateUserViewModel, UpdateUserDtoModel>();
            CreateMap<UpdateUserDtoModel, UpdateUserViewModel>();
            CreateMap<DeleteUserDtoModel, DeleteUserViewModel>();
            CreateMap<DeleteUserDtoModel, UpdatePasswordViewModel>();
            CreateMap<UpdatePasswordViewModel, ChangePasswordDtoModel>();


            // role mappings
            CreateMap<RoleDtoModel, RoleViewModel>();
            CreateMap<UpdateRoleDtoModel, UpdateRoleViewModel>();
            CreateMap<DeleteRoleDtoModel, DeleteRoleViewModel>();
            CreateMap<CreateRoleViewModel, CreateRoleDtoModel>();
            CreateMap<UpdateRoleViewModel, UpdateRoleDtoModel>();


            // account
            CreateMap<LoginViewModel, LoginDtoModel>();
            CreateMap<RegisterViewModel, RegisterDtoModel>();
            CreateMap<AccountProfileDtoModel, AccountProfileViewModel>();


            // claims
            CreateMap<ClaimDtoModel, ClaimViewModel>();
            CreateMap<CrudClaimViewModel, CrudClaimDtoModel>();
            CreateMap<CrudClaimDtoModel, CrudClaimViewModel>();
            CreateMap<DeleteClaimViewModel, DeleteClaimDtoModel>();
            CreateMap<CrudClaimDtoModel, DeleteClaimViewModel>();
        }
    }
}
