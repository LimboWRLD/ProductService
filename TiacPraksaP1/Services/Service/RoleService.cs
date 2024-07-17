using AutoMapper;
using TiacPraksaP1.DTOs.Delete;
using TiacPraksaP1.DTOs.Get;
using TiacPraksaP1.DTOs.Post;
using TiacPraksaP1.Models;
using TiacPraksaP1.Repositories.Interfaces;
using TiacPraksaP1.Services.Interfaces;

namespace TiacPraksaP1.Services.Service
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepositsory, IMapper mapper)
        {
            _roleRepository = roleRepositsory;
            _mapper = mapper;
        }

        public async Task<RolePostResponse> CreateRole(RolePostRequest role)
        {
            return _mapper.Map<RolePostResponse>(await _roleRepository.CreateRole(_mapper.Map<Role>(role)));
        }

        public async Task<RoleDeleteResponse> DeleteRole(int id)
        {
            return _mapper.Map<RoleDeleteResponse>(await _roleRepository.DeleteRole(id));
        }

        public async Task<IEnumerable<RoleGetResponse>> GetAllRoles()
        {
            return _mapper.Map<IEnumerable<RoleGetResponse>>(await _roleRepository.GetRoles());
        }

        public async Task<RoleGetResponse> GetSpecificRole(int id)
        {
            return _mapper.Map<RoleGetResponse>(await _roleRepository.GetRole(id));
        }

        public async Task<RolePostResponse> UpdateRole(RolePostRequest role)
        {
            return _mapper.Map<RolePostResponse>(await _roleRepository.UpdateRole(_mapper.Map<Role>(role)));  
        }
    }
}
