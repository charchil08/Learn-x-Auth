using Microsoft.AspNetCore.Authorization;

namespace WebApp_UnderTheHood.Authorization
{
    public class CoOrdinatorRequirement : IAuthorizationRequirement
    {
        public CoOrdinatorRequirement(int minExperience)
        {
            MinExperience = minExperience;
        }

        public int MinExperience { get; }
    }
}
