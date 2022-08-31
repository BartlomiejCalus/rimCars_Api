using Microsoft.AspNetCore.Authorization;
using rimCars_Api.Entities;

namespace rimCars_Api.Authorization
{
    public class CompanyOwnerRequirementHandler : AuthorizationHandler<CompanyOwnerRequirement>
    {
        private readonly SalonsDbContext _dbContex;

        public CompanyOwnerRequirementHandler(SalonsDbContext dbContex)
        {
            _dbContex = dbContex;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CompanyOwnerRequirement requirement)
        {
            //if (context.User == null)
            //    return Task.CompletedTask;
            var company = context.User?.FindFirst(c => c.Type != null && c.Type == "Company")?.Value;

            var isSame = _dbContex
                .Salons
                .FirstOrDefault(s => s.Address.City == company);

            if (isSame != null)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        } 
    }
}
