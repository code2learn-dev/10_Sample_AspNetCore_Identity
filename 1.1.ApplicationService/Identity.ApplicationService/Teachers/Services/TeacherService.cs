namespace Identity.ApplicationService.Teachers.Services
{
    public class TeacherService

        : BaseDtoService<
                        TeacherService,
                        Teacher,
                        TeacherDtoModel,
                        CreateTeacherDtoModel,
                        UpdateTeacherDtoModel,
                        DeleteTeacherDtoModel>,

        ITeacherService
    {
        public TeacherService(
            ITeacherRepository repository, 
            ITeacherResponse serviceResponse, 
            IMapper mapper, 
            ITeacherMessageMaker messageMaker, 
            ILogger<TeacherService> logger, 
            IModelValidator modelValidator) 
            
            : base(repository, serviceResponse, mapper, messageMaker, logger, modelValidator)
        {
        }
    }
}
