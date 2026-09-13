using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Identity.WebAppRazorPage.Dependencies.Identities.ResourcePolicies
{
    public class ClaimListPolicyRequirement : IAuthorizationRequirement
    {
    }

    public class ClaimListPolicyHandler
        : AuthorizationHandler<
            ClaimListPolicyRequirement,
            IReadOnlyCollection<ClaimViewModel>>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            ClaimListPolicyRequirement requirement, 
            IReadOnlyCollection<ClaimViewModel> resource)
        {
            Claim? claim = context.User.FindFirst("NationalCode");
            if (claim is not null)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
