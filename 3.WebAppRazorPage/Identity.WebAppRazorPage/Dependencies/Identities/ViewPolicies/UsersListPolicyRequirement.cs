using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace Identity.WebAppRazorPage.Dependencies.Identities.ViewPolicies
{
	public class UsersListPolicyRequirement : IAuthorizationRequirement
	{
	}

	public class UsersListPolicyHandler : AuthorizationHandler<UsersListPolicyRequirement, UserViewModel>
	{
		protected override Task HandleRequirementAsync(
			AuthorizationHandlerContext context,
			UsersListPolicyRequirement requirement,
			UserViewModel resource)
		{
			Claim? nationalCodeClaim = context.User.FindFirst("NationalCode");
			if (nationalCodeClaim is not null)
				context.Succeed(requirement);
			else
			{
				Claim? claim = context.User.FindFirst("phonenumber");
				if (claim is not null && claim.Value.Equals(resource.PhoneNumber))
					context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}
