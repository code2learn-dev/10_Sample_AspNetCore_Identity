namespace Identity.ApplicationService.Roles.Response
{
    public interface IRoleResponse
    {
        ApplicationServiceResult<TRoleEntityModel?>
            GetRoleEntityResult<TRoleEntityModel>() where TRoleEntityModel : RoleDtoModel;

        ApplicationServiceResult<IReadOnlyCollection<TRoleEntityModel>>
            GetRolesListResult<TRoleEntityModel>() where TRoleEntityModel : RoleDtoModel;
    }
}
