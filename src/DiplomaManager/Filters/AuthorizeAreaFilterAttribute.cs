using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiplomaManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace DiplomaManager.Filters
{
    public class GroupsRequirement : AuthorizationHandler<GroupsRequirement>, IAuthorizationRequirement
    {
        private readonly IEnumerable<string> _groups;

        public GroupsRequirement(params string[] groups)
        {
            _groups = groups;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GroupsRequirement requirement)
        {
            var mvcContext = context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext;
            if (mvcContext != null)
            {
                var userInfoService = mvcContext.HttpContext.RequestServices.GetService<IUserInfoService>();
                if (userInfoService != null)
                {
                    if (!_groups.Any(role => userInfoService.IsInGroup(role)))
                    {
                        context.Fail();

                        return Task.FromResult<object>(null);
                    }
                    context.Succeed(requirement);

                    return Task.FromResult<object>(null);
                }
                throw new InvalidOperationException("IUserInfoService not found");
            }
            throw new InvalidOperationException("AuthorizationFilterContext not found");
        }
    }
}
