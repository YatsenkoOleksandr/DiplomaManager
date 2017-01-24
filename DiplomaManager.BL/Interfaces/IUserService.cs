using DiplomaManager.BLL.DTOs.UserDTOs;

namespace DiplomaManager.BLL.Interfaces
{
    public interface IUserService
    {
        T GetUser<T>(string login, string password) where T : UserDTO, new();
    }
}
