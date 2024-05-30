using Entities;
using Microsoft.EntityFrameworkCore;
using Respositories;
using System.Text.Json;

namespace Repository
{
    public class UserRepository : IUserRepository
    {

        AvtamContext _dbContext;
        public UserRepository(AvtamContext myShopContext)
        {
            _dbContext = myShopContext;
        }

        public async Task<User> getUser(string email, string password)
        {
            User user = await _dbContext.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> getUserById(int userId)
        {
            User user = await _dbContext.Users.FindAsync(userId);
            return user;
        }

        public async Task<User> saveUser(User user)
        {
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task updateUser(int userId, User newUser)
        {
            _dbContext.Users.Update(newUser);
            await _dbContext.SaveChangesAsync();
        }
    }

}
