using Identity.ApplicationService.Claims.Entities;
using System.Security.Claims;

namespace Identity.ApplicationService.Profiles
{
	public class DtoMappingProfile : Profile
	{
		public DtoMappingProfile()
		{
			CreateMap<Category, CategoryDto>();
			CreateMap<Category, CrudCategoryDto>();
			CreateMap<CrudCategoryDto, Category>();


			CreateMap<Course, CourseDtoModel>();
			CreateMap<Course, UpdateCourseDtoModel>();
			CreateMap<Course, DeleteCourseDtoModel>();
			CreateMap<CreateCourseDtoModel, Course>();
			CreateMap<UpdateCourseDtoModel, Course>();


			CreateMap<Teacher, TeacherDtoModel>();
			CreateMap<Teacher, UpdateTeacherDtoModel>();
			CreateMap<Teacher, DeleteTeacherDtoModel>();
			CreateMap<TeacherDtoModel, Teacher>();
			CreateMap<UpdateTeacherDtoModel, Teacher>();
			CreateMap<CreateTeacherDtoModel, Teacher>();


			// users
			CreateMap<AcademyUser, UserDtoModel>();
			CreateMap<AcademyUser, UpdateUserDtoModel>();
			CreateMap<AcademyUser, DeleteUserDtoModel>();
			CreateMap<CreateUserDtoModel, AcademyUser>()
				.ForMember(dto => dto.Id, opt => opt.Ignore());


			// roles
			CreateMap<AcademyRole, RoleDtoModel>();
			CreateMap<AcademyRole, UpdateRoleDtoModel>();
			CreateMap<AcademyRole, DeleteRoleDtoModel>();
			CreateMap<CreateRoleDtoModel, AcademyRole>()
				.ForMember(dto => dto.Id, opt => opt.Ignore());
			CreateMap<UpdateRoleDtoModel, AcademyRole>();



			// account
			CreateMap<AcademyUser, AccountDtoModel>();
			CreateMap<RegisterDtoModel, AcademyUser>();
			CreateMap<AcademyUser, AccountProfileDtoModel>();


			// claim
			CreateMap<Claim, ClaimDtoModel>()
				.ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.Subject.Name))
				.ForMember(dto => dto.ClaimType, opt => opt.MapFrom(src => src.Type))
				.ForMember(dto => dto.ClaimValue, opt => opt.MapFrom(src => src.Value));


			CreateMap<Claim, CrudClaimDtoModel>() 
				.ForMember(dto => dto.ClaimType, opt => opt.MapFrom(src => src.Type))
				.ForMember(dto => dto.ClaimValue, opt => opt.MapFrom(src => src.Value));

			CreateMap<CrudClaimDtoModel, Claim>()
				.ForMember(dto => dto.Type, opt => opt.MapFrom(src => src.ClaimType))
				.ForMember(dto => dto.Value, opt => opt.MapFrom(src => src.ClaimValue));

			CreateMap<DeleteClaimDtoModel, Claim>()
				.ForMember(dto => dto.Type, opt => opt.MapFrom(src => src.ClaimType))
				.ForMember(dto => dto.Value, opt => opt.MapFrom(src => src.ClaimValue));
		}
	}
}
