using TiacPraksaP1.Models;

namespace TiacPraksaP1.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        public Task<Role> CreateRole(Role role);

        public Task<Role> DeleteRole(int id);

        public Task<Role> UpdateRole(Role role);  

        public Task<Role> GetRole(int id);   
        
        public Task<IEnumerable<Role>> GetRoles();
    }
}
