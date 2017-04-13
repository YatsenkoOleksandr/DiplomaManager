using DiplomaManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaManager.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IUserInfoService UserInfoService { get; set; }

        public AccountController(IUserInfoService userInfoService)
        {
            UserInfoService = userInfoService;
        }

        public string GetUserDisplayName()
        {
            return UserInfoService.GetUserDisplayName();
        }
    }
}