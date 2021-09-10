using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using oceny5._0.Entities;

namespace oceny5._0.Authorization
{
    public class ManageOcenaRequirementHandler : AuthorizationHandler<ManageOcenaRequirement, Ocena>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManageOcenaRequirement requirement, Ocena resource)
        {
            if (requirement.Operation == Operation.Create || requirement.Operation == Operation.Read)
            {
                context.Succeed(requirement);
            }

            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (resource.WykladowcaId == int.Parse(userId))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
