using TiacPraksaP1.DTOs.Delete;
using TiacPraksaP1.DTOs.Get;
using TiacPraksaP1.DTOs.Post;

namespace TiacPraksaP1.Services.Interfaces
{
    public interface IRoleService
    {
        public IEnumerable<RoleGetResponse> GetAllRoles();

        public RoleGetResponse GetSpecificRole(int id);

        public RolePostResponse CreateRole(RolePostRequest role);

        public UserDeleteResponse DeleteRole(int id);

        public RolePostResponse UpdateRole(RolePostRequest role);
    }
}
