using Entities;

namespace ServiceLayer
{
    public interface IUserService
    {
        Task<User> getUser(string email, string password);
        Task<User> getUserById(int userId);
        Task<User> saveUser(User user);
        Task updateUser(int userId,User user);
    }
}