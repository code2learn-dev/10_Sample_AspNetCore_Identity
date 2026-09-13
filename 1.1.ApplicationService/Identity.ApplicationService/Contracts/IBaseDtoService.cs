namespace Identity.ApplicationService.Contracts
{
    public interface IBaseDtoService<
        TService,
        TEntity, 
        TEntityDtoModel,
        TCreateEntityDtoModel,
        TUpdateEntityDtoModel,
        TDeleteEntityDtoModel>  
        where TEntity : BaseEntity
        where TEntityDtoModel : BaseEntityDto
        where TCreateEntityDtoModel : BaseEntityDto
        where TUpdateEntityDtoModel : BaseEntityDto
        where TDeleteEntityDtoModel : BaseEntityDto
        where TService : class
    {
        ApplicationServiceResult<IReadOnlyCollection<TEntityDtoModel>> GetAllEntityDtos();

        Task<ApplicationServiceResult<IReadOnlyCollection<TEntityDtoModel>>> GetAllEntityDtosAsync(string errorMessage = "خطا در خواندن لیست داده ها");

        Task<ApplicationServiceResult<TEntityDtoModel?>> AddEntityDtoAsync(TCreateEntityDtoModel? model);

        Task<ApplicationServiceResult<TEntityDtoModel?>> UpdateEntityDtoAsync(TUpdateEntityDtoModel? model);

        Task<ApplicationServiceResult<TEntityDtoModel?>> DeleteEntityDtoAsync(long? id);

        Task<ApplicationServiceResult<TEntityDtoModel?>> FindByIdEntityDtoAsync(long? id);

        ApplicationServiceResult<TEntityDtoModel?> FindByIdEntityDto(long? id);

        Task<ApplicationServiceResult<TUpdateEntityDtoModel?>> FindByIdUpdateEntityDtoAsync(long? id);

        Task<ApplicationServiceResult<TDeleteEntityDtoModel?>> FindByIdDeleteEntityDtoAsync(long? id);

	}
}
