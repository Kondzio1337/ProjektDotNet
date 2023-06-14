using Microsoft.AspNetCore.Authorization;

namespace Projekt.Autoryzacja
{
    public class KierownikCosTam: IAuthorizationRequirement
    {
        public KierownikCosTam(int probationMonths)
        {
            ProbationMonths = probationMonths;
        }
        public int ProbationMonths { get; set;}
    }
    public class KierownikHandler : AuthorizationHandler<KierownikCosTam>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, KierownikCosTam requirement)
        {
            if(!context.User.HasClaim(x=> x.Type == "EmploymentDate"))
            {
                return Task.CompletedTask;
            }
            var empDate = DateTime.Parse(context.User.FindFirst(x => x.Type == "EmploymentDate").Value);
            var period = DateTime.Now - empDate;
            if (period.Days > 30 * requirement.ProbationMonths)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
