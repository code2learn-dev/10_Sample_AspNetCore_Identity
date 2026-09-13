namespace Identity.ApplicationService.Teachers.Services
{
    public interface ITeacherService  

        : IBaseDtoService<
                          TeacherService,
                          Teacher,
                          TeacherDtoModel,
                          CreateTeacherDtoModel,
                          UpdateTeacherDtoModel,
                          DeleteTeacherDtoModel>
    {
    }
}
