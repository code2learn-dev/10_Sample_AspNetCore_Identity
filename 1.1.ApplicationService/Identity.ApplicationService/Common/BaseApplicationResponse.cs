namespace Identity.ApplicationService.Common
{
    public abstract class BaseApplicationResponse : IServiceResponse 
    {
        public ApplicationServiceResult<IReadOnlyCollection<TEntityResult>> 
            GetAllEntitiesResultResponse<TEntityResult>()
            where TEntityResult : BaseEntityDto
            => new();

        public ApplicationServiceResult<TEntityResult?> 
            GetEntityResultResponse<TEntityResult>()
            where TEntityResult : BaseEntityDto
            => new();

        public ApplicationServiceResult<int> GetResultResponse() => new();
    }
}
