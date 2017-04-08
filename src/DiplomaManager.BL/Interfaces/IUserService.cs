using System.Collections.Generic;
using DiplomaManager.DAL.Entities.UserEnitites;

namespace DiplomaManager.BLL.Interfaces
{
    public interface IUserService
    {
        T GetUser<T>(string login) where T : User;

        string CreateRandomPassword(int passwordLength);

        T GetUserFromFullName<T>(ICollection<PeopleName> peopleNames) where T : User;
    }
}
