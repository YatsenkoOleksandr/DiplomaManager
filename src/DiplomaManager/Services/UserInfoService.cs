using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace DiplomaManager.Services
{
    public interface IUserInfoService
    {
        bool IsInGroup(string groupName);

        bool IsInGroup(List<string> groupNames);

        WindowsIdentity GetUserIdWithDomain();

        string GetUserId();
    }

    public class UserInfoService : IUserInfoService
    {
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

        public WindowsIdentity GetUserIdWithDomain()
        {
            var myIdentity = WindowsIdentity.GetCurrent();
            return myIdentity;
        }

        public string GetUserId()
        {
            var id = GetUserIdWithDomain().Name.Split('\\');
            return id[1];
        }
    }
}
