using Identity.ApplicationService.Claims.Entities;

namespace Identity.ApplicationService.Claims.Services
{
    public interface IClaimService
    {
        Task<ApplicationServiceResult<ClaimDtoModel?>> AddAsync(CrudClaimDtoModel model);
        Task<ApplicationServiceResult<ClaimDtoModel?>> DeleteAsync(DeleteClaimDtoModel model);
        Task<ApplicationServiceResult<ClaimDtoModel?>> EditClaimAsync(CrudClaimDtoModel model);

        Task<(ApplicationServiceResult<CrudClaimDtoModel?>, AcademyUser?)> FindClaimByType(string? claimType);

        Task<ApplicationServiceResult<IReadOnlyCollection<ClaimDtoModel>>> GetAllUserClaims();

        Task<ApplicationServiceResult<string?>> GetCurrentUserIdAsync();

        ApplicationServiceResult<IReadOnlyCollection<ClaimDtoModel>> GetSystemClaims();
    }
}