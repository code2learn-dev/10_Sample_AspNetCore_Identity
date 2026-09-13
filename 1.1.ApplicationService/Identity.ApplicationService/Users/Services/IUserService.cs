namespace Identity.ApplicationService.Users.Services
{
    public interface IUserService
    {
        ApplicationServiceResult<IReadOnlyCollection<UserDtoModel>> ReadUsers();

        Task<ApplicationServiceResult<IReadOnlyCollection<UserDtoModel>>> ReadUsersAsync(); 

        Task<ApplicationServiceResult<UpdateUserDtoModel?>> FindUpdateUserByIdAsync(string? userId);

        Task<ApplicationServiceResult<DeleteUserDtoModel?>> FindDeleteUserByIdAsync(string? userId);

        Task<ApplicationServiceResult<UserDtoModel?>> CreateUserAsync(CreateUserDtoModel model);

        Task<ApplicationServiceResult<UserDtoModel?>> UpdateUserAsync(UpdateUserDtoModel model);

        Task<ApplicationServiceResult<UserDtoModel?>> DeleteUserAsync(string userId);

        Task<ApplicationServiceResult<UserDtoModel?>> ChangePasswordAsync(ChangePasswordDtoModel model);
    } 
}
