using TiacPraksaP1.Models;

namespace TiacPraksaP1.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        public Role CreateRole(Role role);

        public Role DeleteRole(int id);

        public Role UpdateRole(Role role);  

        public Role GetRole(int id);   
        
        public IEnumerable<Role> GetRoles();
    }
}
