namespace Identity.ApplicationService.Roles.Response
{
    public class RoleResponse : IRoleResponse
    {
        public ApplicationServiceResult<TRoleEntityModel?>
            GetRoleEntityResult<TRoleEntityModel>() where TRoleEntityModel : RoleDtoModel
            => new();


        public ApplicationServiceResult<IReadOnlyCollection<TRoleEntityModel>>
            GetRolesListResult<TRoleEntityModel>() where TRoleEntityModel : RoleDtoModel
            => new();
        
    }
}
