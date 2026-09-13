using Identity.ApplicationService.Claims.Entities;
using Identity.ApplicationService.Claims.Response;
using Identity.ApplicationService.Claims.Validators;
using Identity.ApplicationService.Users.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.ApplicationService.Claims.Services
{
    public class ClaimService : IClaimService
    {
        private readonly UserManager<AcademyUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IClaimValidator _claimValidator;
        private readonly IClaimMessageMaker _claimMessageMaker;
        private ILogger<UserService> _logger;

        public ClaimService(
            UserManager<AcademyUser> userManager,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IClaimValidator claimValidator,
            IClaimMessageMaker claimMessageMaker,
            ILogger<UserService> logger)
        {
            _userManager = userManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _claimValidator = claimValidator;
            _claimMessageMaker = claimMessageMaker;
            _logger = logger;
        }

        public async Task<ApplicationServiceResult<IReadOnlyCollection<ClaimDtoModel>>> GetAllUserClaims()
        {
            ApplicationServiceResult<IReadOnlyCollection<ClaimDtoModel>> claimResult = new();

            ClaimsPrincipal currentUser = _httpContextAccessor.HttpContext.User;
            AcademyUser? user = await _userManager.GetUserAsync(currentUser);
            if (user is null)
            {
                claimResult.AddError("کاربری یافت نشد");
                return claimResult;
            }

            IList<Claim> claims = await _userManager.GetClaimsAsync(user);
            claimResult.AddResult(_mapper.Map<IReadOnlyCollection<ClaimDtoModel>>(claims));
            return claimResult;
        }

        public ApplicationServiceResult<IReadOnlyCollection<ClaimDtoModel>> GetSystemClaims()
        {
            ApplicationServiceResult<IReadOnlyCollection<ClaimDtoModel>> claimResult = new();
            try
            {
                ClaimsPrincipal currentUser = _httpContextAccessor.HttpContext.User;

                IEnumerable<Claim> claims = currentUser.Claims;
                claimResult.AddResult(_mapper.Map<IReadOnlyCollection<ClaimDtoModel>>(claims));
                return claimResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _claimMessageMaker.SetMessage(ClaimCrudType.read, HttpStatusCode.NotFound);
                claimResult.AddError(_claimMessageMaker.Message);
                return claimResult;
            }
		}

		public async Task<ApplicationServiceResult<ClaimDtoModel?>> AddAsync(CrudClaimDtoModel model)
        {
            ApplicationServiceResult<ClaimDtoModel?> claimResult = new();
            var validationResult = await _claimValidator.ValidateClaimAsync(model);
            if (!validationResult.IsSuccess) return validationResult;

            if (string.IsNullOrEmpty(model.UserId))
            {
                _claimMessageMaker.SetMessage(ClaimCrudType.add, HttpStatusCode.BadRequest);
                claimResult.AddError(_claimMessageMaker.Message);
                return claimResult;
            }

            AcademyUser? user = await _userManager.FindByIdAsync(model.UserId);
            if (user is null)
            {
                _claimMessageMaker.SetMessage(ClaimCrudType.add, HttpStatusCode.BadRequest);
                claimResult.AddError(_claimMessageMaker.Message);
                return claimResult;
            }

            Claim claim = new Claim(model.ClaimType, model.ClaimValue); 
            IdentityResult identityResult = await _userManager.AddClaimAsync(user, claim);
            if (!identityResult.Succeeded)
            {
                identityResult.LoggUserIdentityErrors(_logger);
                List<string> identityErrors = identityResult.GetIdentityErrors();
                claimResult.AddErrorsList([.. identityErrors]);
            }
            else
            {
                _claimMessageMaker.SetMessage(ClaimCrudType.add, HttpStatusCode.OK);
                claimResult.AddMessage(_claimMessageMaker.Message);
            }

            return claimResult;
        }



        public async Task<ApplicationServiceResult<ClaimDtoModel?>> EditClaimAsync(CrudClaimDtoModel model)
        {
            ApplicationServiceResult<ClaimDtoModel?> editResult = new();
            ApplicationServiceResult<ClaimDtoModel?> claimValidationResult = await _claimValidator.ValidateClaimAsync(model);
            if (!claimValidationResult.IsSuccess) return claimValidationResult;

            ClaimsPrincipal currentUser = _httpContextAccessor.HttpContext.User;
            AcademyUser? user = await _userManager.GetUserAsync(currentUser);
            if(user is null)
            {
                _claimMessageMaker.SetMessage(ClaimCrudType.replace, HttpStatusCode.NotFound);
                editResult.AddError(_claimMessageMaker.Message);
                return editResult;
            }

            IList<Claim> claims = await _userManager.GetClaimsAsync(user);
            Claim? claim = claims.FirstOrDefault(a => a.Type.Equals(model.ClaimType, StringComparison.OrdinalIgnoreCase));
            if(claim is null)
            {
				_claimMessageMaker.SetMessage(ClaimCrudType.replace, HttpStatusCode.NotFound);
				editResult.AddError(_claimMessageMaker.Message);
				return editResult;
			}

            Claim newClaim = new Claim(model.ClaimType, model.ClaimValue);
			IdentityResult identityResult = 
                await _userManager.ReplaceClaimAsync(
                user,
                claim,
                newClaim);
            if(!identityResult.Succeeded)
            {
                identityResult.LoggUserIdentityErrors(_logger);
                List<string> errors = identityResult.GetIdentityErrors();
                editResult.AddErrorsList(errors.ToArray());
                _claimMessageMaker.SetMessage(ClaimCrudType.replace, HttpStatusCode.BadRequest);
                editResult.AddError(_claimMessageMaker.Message);
            }
            else
            {
                _claimMessageMaker.SetMessage(ClaimCrudType.replace, HttpStatusCode.OK);
                editResult.AddMessage(_claimMessageMaker.Message);
                editResult.AddResult(_mapper.Map<ClaimDtoModel>(newClaim));
            }

            return editResult;
		}

        public async Task<ApplicationServiceResult<ClaimDtoModel?>> DeleteAsync(DeleteClaimDtoModel model)
        {
            ApplicationServiceResult<ClaimDtoModel?> deleteResult = new();

            AcademyUser? user = await _userManager.FindByIdAsync(model.UserId);
            if(user is null)
            {
                deleteResult.AddError("کاربری یافت نشد");
                return deleteResult;
            }

            Claim? claim = (await _userManager.GetClaimsAsync(user))
                .SingleOrDefault(a => a.Type.Equals(model.ClaimType, StringComparison.OrdinalIgnoreCase));
            IdentityResult identityResult = await _userManager.RemoveClaimAsync(user, claim);
            if(!identityResult.Succeeded)
            {
                identityResult.LoggUserIdentityErrors(_logger);
                List<string> errors = identityResult.GetIdentityErrors();
                deleteResult.AddErrorsList([.. errors]);
            }
            else
            {
                _claimMessageMaker.SetMessage(ClaimCrudType.delete, HttpStatusCode.OK);
                deleteResult.AddMessage(_claimMessageMaker.Message);
            }

            return deleteResult;
        }

        public async Task<(ApplicationServiceResult<CrudClaimDtoModel?>, AcademyUser?)> FindClaimByType(string? claimType)
        {
            ApplicationServiceResult<CrudClaimDtoModel?> claimResult = new();
            if(string.IsNullOrEmpty(claimType))
            {
                _claimMessageMaker.SetMessage(ClaimCrudType.replace, HttpStatusCode.NotFound);
                claimResult.AddError(_claimMessageMaker.Message);
                return (claimResult, null);
            }

			ClaimsPrincipal currentUser = _httpContextAccessor.HttpContext.User;
			AcademyUser? user = await _userManager.GetUserAsync(currentUser);
			if (user is null)
			{ 
				claimResult.AddError("کاربری یافت نشد");
				return (claimResult, null);
			}

            IList<Claim> claims = await _userManager.GetClaimsAsync(user);
            Claim? claim = claims.FirstOrDefault(a => a.Type.Equals(claimType, StringComparison.OrdinalIgnoreCase));
            if(claim is null)
            {
                _claimMessageMaker.SetMessage(ClaimCrudType.read, HttpStatusCode.NotFound);
                claimResult.AddError(_claimMessageMaker.Message);
                return (claimResult, user);
            }

            CrudClaimDtoModel crudClaim = _mapper.Map<CrudClaimDtoModel>(claim);
            crudClaim.UserId = user.Id;
			claimResult.AddResult(crudClaim);
            return (claimResult, user);
		}

        public async Task<ApplicationServiceResult<string?>> GetCurrentUserIdAsync()
        {
            ApplicationServiceResult<string?> userIdResult = new();
            ClaimsPrincipal currentUser = _httpContextAccessor.HttpContext.User;
            AcademyUser? user = await _userManager.GetUserAsync(currentUser);

            if(user is null)
            {
                userIdResult.AddError("کاربری یافت نشد");
                return userIdResult;
            }

            userIdResult.AddResult(user.Id);
            return userIdResult;
        }
    }
}
