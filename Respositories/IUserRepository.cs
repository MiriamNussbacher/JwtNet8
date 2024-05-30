using Entities;

namespace Repository
{
    public interface IUserRepository
    {
        Task<User> getUser(string email, string password);
        Task<User> getUserById(int userId);
        Task<User> saveUser(User user);
        Task updateUser(int userId, User user);

    }
}