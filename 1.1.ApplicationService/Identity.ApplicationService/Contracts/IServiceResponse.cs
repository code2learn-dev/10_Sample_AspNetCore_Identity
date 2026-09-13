namespace Identity.ApplicationService.Contracts
{
    public interface IServiceResponse
    {
        ApplicationServiceResult<IReadOnlyCollection<TEntityResult>> 
            GetAllEntitiesResultResponse<TEntityResult>()
            where TEntityResult : BaseEntityDto;

        ApplicationServiceResult<TEntityResult?> 
            GetEntityResultResponse<TEntityResult>()
            where TEntityResult : BaseEntityDto;

        ApplicationServiceResult<int> GetResultResponse();
    }
}
