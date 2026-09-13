namespace Identity.ApplicationService.Users.Response
{
    public class UserResponse : IUserResponse
    {
        public ApplicationServiceResult<IReadOnlyCollection<TUserEntityModel>> 
            GetEntitiesListResultResponse<TUserEntityModel>() where TUserEntityModel : UserDtoModel
			=> new();

        public ApplicationServiceResult<TUserDtoModel?>
            GetEntityResultResponse<TUserDtoModel>() where TUserDtoModel : UserDtoModel
			=> new();
    }
}
