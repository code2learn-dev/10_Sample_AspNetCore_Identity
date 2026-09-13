using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Identity.WebAppRazorPage.Dependencies.Identities.ResourcePolicies
{
    public class TeacherListPolicyRequirement : IAuthorizationRequirement
    {

    }

    public class TeacherListPolicyHandler : AuthorizationHandler<
        TeacherListPolicyRequirement,
		IReadOnlyCollection<TeacherViewModel>>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            TeacherListPolicyRequirement requirement,
			IReadOnlyCollection<TeacherViewModel> resource)
        {
            Claim? claim = context.User.FindFirst("NationalCode");
            if (claim is not null)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
