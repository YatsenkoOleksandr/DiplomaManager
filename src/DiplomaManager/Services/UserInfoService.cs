using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using DiplomaManager.BLL.Interfaces;

namespace DiplomaManager.Services
{
    public interface IUserInfoService
    {
        bool IsInGroup(string groupName);

        bool IsInGroup(List<string> groupNames);

        string GetUserId();

        string GetUserDisplayName();
    }

    public class UserInfoService : IUserInfoService
    {
        private IUserService UserService { get; }
        
        public UserInfoService(IUserService userService)
        {
            UserService = userService;
        }

        public bool IsInGroup(string groupName)
        {
            var myIdentity = GetUserIdWithDomain();
            var myPrincipal = new WindowsPrincipal(myIdentity);
            return myPrincipal.IsInRole(groupName);
        }

        public bool IsInGroup(List<string> groupNames)
        {
            var myIdentity = GetUserIdWithDomain();
            var myPrincipal = new WindowsPrincipal(myIdentity);

            return groupNames.Any(group => myPrincipal.IsInRole(group));
        }

        public string GetUserId()
        {
            var id = GetUserIdWithDomain().Name.Split('\\');
            return id[1];
        }

        public string GetUserDisplayName()
        {
            var userId = GetUserId();
            var displayName = UserService.GetUserDisplayName(userId);
            return !string.IsNullOrWhiteSpace(displayName) 
                ? UserService.GetUserDisplayName(userId) 
                : userId;
        }

        private WindowsIdentity GetUserIdWithDomain()
        {
            var myIdentity = WindowsIdentity.GetCurrent();
            return myIdentity;
        }
    }
}
