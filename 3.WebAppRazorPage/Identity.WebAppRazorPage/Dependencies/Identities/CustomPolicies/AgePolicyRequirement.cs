using Microsoft.AspNetCore.Authorization;

namespace Identity.WebAppRazorPage.Dependencies.Identities.CustomPolicies
{
    public class AgePolicyRequirement : IAuthorizationRequirement
    {
        public int Age { get; }

        public AgePolicyRequirement(int age)
        {
            Age = age;
        }
    }

    public class AgePolicyHandler : AuthorizationHandler<AgePolicyRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            AgePolicyRequirement requirement)
        {
            var claimDate = context.User.FindFirst("birthdate");
            if (claimDate is null)
                return Task.CompletedTask;

            var birthDateYear = int.TryParse(claimDate.Value, out int br) ? br : 0;
            if (birthDateYear == 0)
                return Task.CompletedTask;

            var age = DateTime.Now.Year - birthDateYear;
            if (age >= requirement.Age)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
