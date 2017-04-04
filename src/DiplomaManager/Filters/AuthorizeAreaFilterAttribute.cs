using System;
using System.Collections.Generic;
using System.Linq;
using DiplomaManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace DiplomaManager.Filters
{
    internal class Http403Result : ActionResult
    {
        public override void ExecuteResult(ActionContext actionContext)
        {
            actionContext.HttpContext.Response.StatusCode = 403;
        }
    }

    public class AuthorizeAreaFilterAttribute : ActionFilterAttribute
    {
        private readonly string _areaName;
        private readonly IEnumerable<string> _roles;

        public AuthorizeAreaFilterAttribute(string areaName, IEnumerable<string> roles)
        {
            _areaName = areaName;
            _roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var area = context.RouteData.Values["area"];
            var areaStr = area?.ToString();
            if (!string.IsNullOrWhiteSpace(areaStr) && areaStr == _areaName.ToLower())
            {
                var userInfoService = context.HttpContext.RequestServices.GetService<IUserInfoService>();
                if (userInfoService != null)
                {
                    if (!_roles.Any(role => userInfoService.IsInGroup(role)))
                    {
                        context.Result = new Http403Result();
                    }
                    return;
                }
                throw new InvalidOperationException("IUserInfoService not found");
            }
        }
    }
}
