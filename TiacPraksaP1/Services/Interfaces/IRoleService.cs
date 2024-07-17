using TiacPraksaP1.DTOs.Delete;
using TiacPraksaP1.DTOs.Get;
using TiacPraksaP1.DTOs.Post;

namespace TiacPraksaP1.Services.Interfaces
{
    public interface IRoleService
    {
        public Task<IEnumerable<RoleGetResponse>> GetAllRoles();

        public Task<RoleGetResponse> GetSpecificRole(int id);

        public Task<RolePostResponse> CreateRole(RolePostRequest role);

        public Task<RoleDeleteResponse> DeleteRole(int id);

        public Task<RolePostResponse> UpdateRole(RolePostRequest role);
    }
}
