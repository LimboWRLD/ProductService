using System.Data;
using TiacPraksaP1.Data;
using TiacPraksaP1.Models;
using TiacPraksaP1.Repositories.Interfaces;

namespace TiacPraksaP1.Repositories.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public Role CreateRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role;
        }

        public Role DeleteRole(int id)
        {
            var role =  _context.Roles.FirstOrDefault(x => x.Id == id);

            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();

            }
            return role;
        }

        public Role GetRole(int id)
        {
            return _context.Roles.FirstOrDefault(x=> x.Id == id);

        }

        public IEnumerable<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public Role UpdateRole(Role role)
        {
            var foundRole  = GetRole(role.Id);

            if (foundRole != null)
            {
                foundRole.Name = role.Name!;
                _context.SaveChanges();
                return foundRole;
            }
            return null;
        }
    }
}
