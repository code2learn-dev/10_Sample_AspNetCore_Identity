using Identity.ApplicationService.Roles.Services;
using Identity.ApplicationService.Users.Services;

namespace Identity.ApplicationService.Utilities
{
    public static class ValidationExtensions
    {
        public static List<string> GetValidationResultErrors(this ValidationResult? validationResult)
        {
            if (validationResult is null || validationResult.IsValid) return [];

            return [.. validationResult.Errors.Select(e => e.ErrorMessage)];
        }

        public static List<string> GetIdentityErrors(this IdentityResult? identityResult)
        {
            if (identityResult is null || identityResult.Succeeded) return [];

            return [.. identityResult.Errors.Select(e => e.Description)];
        }

        public static void LoggUserIdentityErrors(this IdentityResult? identityResult, ILogger<UserService> logger)
        {
            if (identityResult is null || identityResult.Succeeded) return;

            List<string> errors = [.. identityResult.Errors.Select(a => a.Description)]; 
            logger.LogError(string.Join(" | ", errors));
        }

		public static void LoggRoleIdentityErrors(this IdentityResult? identityResult, ILogger<RoleService> logger)
		{
			if (identityResult is null || identityResult.Succeeded) return;

			List<string> errors = [.. identityResult.Errors.Select(a => a.Description)];
			logger.LogError(string.Join(" | ", errors));
		}
	}
}
