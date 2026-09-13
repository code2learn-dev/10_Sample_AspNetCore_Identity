using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Identity.WebAppRazorPage.Dependencies.Identities.CustomPolicies
{
    public class NationalCodePolicyRequirement : IAuthorizationRequirement
    {

    }

    public class NationaCodePolicyHandler : AuthorizationHandler<NationalCodePolicyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, NationalCodePolicyRequirement requirement)
        {
            Claim? claim = context.User.FindFirst("NationalCode");
            if (claim is not null)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
