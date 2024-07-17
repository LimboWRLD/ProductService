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

        public RolePostResponse CreateRole(RolePostRequest role)
        {
            return _mapper.Map<RolePostResponse>(_roleRepository.CreateRole(_mapper.Map<Role>(role)));
        }

        public UserDeleteResponse DeleteRole(int id)
        {
            return _mapper.Map<UserDeleteResponse>(_roleRepository.DeleteRole(id));
        }

        public IEnumerable<RoleGetResponse> GetAllRoles()
        {
            return _mapper.Map<IEnumerable<RoleGetResponse>>(_roleRepository.GetRoles());
        }

        public RoleGetResponse GetSpecificRole(int id)
        {
            return _mapper.Map<RoleGetResponse>(_roleRepository.GetRole(id));
        }

        public RolePostResponse UpdateRole(RolePostRequest role)
        {
            return _mapper.Map<RolePostResponse>(_roleRepository.UpdateRole(_mapper.Map<Role>(role)));  
        }
    }
}
