namespace Identity.ApplicationService.Categories.Services
{
    public class CategoryService
        : BaseDtoService<
            CategoryService,
            Category,
            CategoryDto,
            CrudCategoryDto,
            CrudCategoryDto,
            CategoryDto>,

        ICategoryService
    {
        public CategoryService(
            ICategoryRepository repository, 
            ICategoryResponse categoryResponse, 
            IMapper mapper, 
            ICategoryMessageMaker messageMaker, 
            ILogger<CategoryService> logger, 
            IModelValidator modelValidator) 
            : base(repository, categoryResponse, mapper, messageMaker, logger, modelValidator)
        {
        }
    }
}
