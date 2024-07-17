using Microsoft.EntityFrameworkCore;
using System.Data;
using TiacPraksaP1.Data;
using TiacPraksaP1.Models;
using TiacPraksaP1.Repositories.Interfaces;

namespace TiacPraksaP1.Repositories.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Role> CreateRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<Role> DeleteRole(int id)
        {
            var role =  _context.Roles.FirstOrDefault(x => x.Id == id);

            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();

            }
            return role;
        }

        public async Task<Role> GetRole(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(x=> x.Id == id);

        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> UpdateRole(Role role)
        {
            var foundRole  = await GetRole(role.Id);

            if (foundRole != null)
            {
                foundRole.Name = role.Name!;
                await _context.SaveChangesAsync();
                return foundRole;
            }
            return null;
        }
    }
}
