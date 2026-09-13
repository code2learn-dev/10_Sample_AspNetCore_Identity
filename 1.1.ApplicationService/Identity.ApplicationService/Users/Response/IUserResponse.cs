namespace Identity.ApplicationService.Users.Response
{
    public interface IUserResponse
    {
        ApplicationServiceResult<TUserDtoModel?> 
            GetEntityResultResponse<TUserDtoModel>() where TUserDtoModel : UserDtoModel;

        ApplicationServiceResult<IReadOnlyCollection<TUserEntityModel>>
            GetEntitiesListResultResponse<TUserEntityModel>() where TUserEntityModel : UserDtoModel;

	}
}
