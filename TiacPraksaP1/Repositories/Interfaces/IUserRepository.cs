using TiacPraksaP1.Models;

namespace TiacPraksaP1.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public User CreateUser(User user);

        public User DeleteUser(int id);

        public User UpdateUser(User role);

        public User GetUser(int id);

        public IEnumerable<User> GetUsers();
    }
}
