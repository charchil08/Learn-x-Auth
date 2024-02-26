using Microsoft.AspNetCore.Authorization;
using WebApp_UnderTheHood.Entities.Helper;

namespace WebApp_UnderTheHood.Authorization
{
    public class CoOrdinatorRequirementHandler : AuthorizationHandler<CoOrdinatorRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CoOrdinatorRequirement requirement)
        {
            if (!context.User.HasClaim(claim => claim.Type == Constants.JoinedDateClaimKey))
            {
                return Task.CompletedTask;
            }

            var claimValue = context.User.Claims.FirstOrDefault(claim => claim.Type == Constants.JoinedDateClaimKey)?.Value;
            if (string.IsNullOrWhiteSpace(claimValue))
            {
                return Task.CompletedTask;
            }
            DateTime joinedAt = DateTime.Parse(claimValue);
            DateTime currentDate = DateTime.UtcNow;

            if ((currentDate - joinedAt).Days > 30 * requirement.MinExperience)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
