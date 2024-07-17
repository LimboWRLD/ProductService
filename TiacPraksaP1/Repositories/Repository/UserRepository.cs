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

        public User CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User DeleteUser(int id)
        {
            var foundUser = _context.Users.FirstOrDefault(x => x.Id == id);
            if (foundUser != null)
            {
                _context.Users.Remove(foundUser);
                _context.SaveChanges();
            }
            return foundUser;
        }

        public User GetUser(int id) => _context.Users.FirstOrDefault(x => x.Id == id);

        public IEnumerable<User> GetUsers() => _context.Users;
    

        public User UpdateUser(User user)
        {
            var foundUser = GetUser(user.Id);
            if (foundUser != null)
            {
                foundUser.Username = user.Username;
                foundUser.Password = user.Password;
                foundUser.Email = user.Email;
                foundUser.Role = user.Role;
                _context.SaveChanges();
            }
            return user;
        }
    }
}
