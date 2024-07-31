using AutoMapper;
using BusinessLogicLayer.DTOs.Delete;
using BusinessLogicLayer.DTOs.Get;
using BusinessLogicLayer.DTOs.Post;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Service
{
    public class UserProductService : IUserProductService
    {
        private readonly IUserProductsRepository _userProductRepository;
        private readonly IMapper _mapper;

        public UserProductService(IUserProductsRepository userProductRepository, IMapper mapper)
        {
            _userProductRepository = userProductRepository;
            _mapper = mapper;
        }

        public async Task<UserProductPostResponse> CreateUserProduct(UserProductPostRequest UserProduct)
        {
           return _mapper.Map<UserProductPostResponse>(await _userProductRepository.CreateUserProduct(_mapper.Map<UserProduct>(UserProduct)));
        }

        public async Task<UserProductDeleteResponse> DeleteUserProduct(int userProductId)
        {
            return _mapper.Map<UserProductDeleteResponse>(await _userProductRepository.DeleteUserProduct(userProductId));
        }

        public async Task<IEnumerable<UserProductGetResponse>> GetAllUserProducts()
        {
            return _mapper.Map<IEnumerable<UserProductGetResponse>>(await _userProductRepository.GetUserProducts());
        }

        public async Task<UserProductGetResponse> GetSpecificUserProduct(int ProductId)
        {
            return _mapper.Map<UserProductGetResponse>(await _userProductRepository.GetUserProduct(ProductId));
        }


        public async Task<UserProductPostResponse> UpdateUserProduct(int ProductId, UserProductPostRequest product)
        {
            return _mapper.Map<UserProductPostResponse>(await _userProductRepository.UpdateUserProduct(ProductId, _mapper.Map<UserProduct>(product)));
        }
    }
}
