using Microsoft.EntityFrameworkCore;
using TiacPraksaP1.Data;
using TiacPraksaP1.Models;
using TiacPraksaP1.Repositories.Interfaces;

namespace TiacPraksaP1.Repositories.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext appDbContext)
        {
            this._context = appDbContext;
        }

        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteUser(int id)
        {
            var foundUser = _context.Users.FirstOrDefault(x => x.Id == id);
            if (foundUser != null)
            {
                _context.Users.Remove(foundUser);
                await _context.SaveChangesAsync();
            }
            return foundUser;
        }

        public async Task<User> GetUser(int id) => _context.Users.FirstOrDefault(x => x.Id == id);

        public async Task<IEnumerable<User>> GetUsers() => await _context.Users.ToListAsync();
    

        public async Task<User> UpdateUser(User user)
        {
            var foundUser =await GetUser(user.Id);
            if (foundUser != null)
            {
                foundUser.Username = user.Username;
                foundUser.Password = user.Password;
                foundUser.Email = user.Email;
                foundUser.Role = user.Role;
                await _context.SaveChangesAsync();
            }
            return user;
        }
    }
}
