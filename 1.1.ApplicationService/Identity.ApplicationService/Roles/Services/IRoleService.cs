namespace Identity.ApplicationService.Roles.Services
{
    public interface IRoleService
    {
        ApplicationServiceResult<IReadOnlyCollection<RoleDtoModel>> ReadAllRoles();

        Task<ApplicationServiceResult<RoleDtoModel?>> CreateAsync(CreateRoleDtoModel model);

        Task<ApplicationServiceResult<RoleDtoModel?>> Update(UpdateRoleDtoModel model);

        Task<ApplicationServiceResult<RoleDtoModel?>> Delete(string? roleId);

        Task<ApplicationServiceResult<DeleteRoleDtoModel?>> FindDeleteRoleByIdAsync(string? roleId);

        Task<ApplicationServiceResult<UpdateRoleDtoModel?>> FindUpdateRoleByIdAsync(string? roleId);
    }

    public enum AccountRole : byte
    {
        admin = 1,
        member,
        teacher
    }
}
