using AutoMapper;
using BusinessLogicLayer.DTOs.Delete;
using BusinessLogicLayer.DTOs.Get;
using BusinessLogicLayer.DTOs.Post;
using BusinessLogicLayer.Services.Service;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository.Interfaces;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiacPraksaP1.DTOs.Get;

namespace Tests.ServiceTests
{
    public class UserProductServiceTests
    {
        private readonly IUserProductsRepository _userProductsRepository;
        private readonly IMapper _mapper;
        private readonly UserProductService _userProductService;

        public UserProductServiceTests()
        {
            _userProductsRepository = Substitute.For<IUserProductsRepository>();
            _mapper = Substitute.For<IMapper>();
            _userProductService = new UserProductService(_userProductsRepository, _mapper);
        }

        [Fact]
        public async Task Add_UserProduct_Should_Be_NotNull()
        {
            var userProduct = new UserProduct {UserId = "1", ProductId = 1 };
            var userProductPostRequest = new UserProductPostRequest { ProductId = 1 };
            var userProductPostResponse = new UserProductPostResponse {  ProductId = 1 , UserId = "1"};

            _mapper.Map<UserProduct>(userProductPostRequest).Returns(userProduct);
            _userProductsRepository.CreateUserProduct(userProduct).Returns(Task.FromResult(userProduct));   
            _mapper.Map<UserProductPostResponse>(userProduct).Returns(userProductPostResponse);

            var result  = await _userProductService.CreateUserProduct(userProductPostRequest);

            result.Should().NotBeNull();    
            result.Should().BeEquivalentTo(userProductPostResponse);
        }

        [Fact]
        public async Task Add_UserProduct_Should_Be_Null()
        {
            var userProduct = new UserProduct { UserId = "1", ProductId = 1 };
            var userProductPostRequest = new UserProductPostRequest { ProductId = 1 };

            _userProductsRepository.CreateUserProduct(userProduct).Returns(Task.FromResult<UserProduct>(null));

            var result =await _userProductService.CreateUserProduct(userProductPostRequest);
            
            result?.Should().BeNull();
        }

        [Fact]
        public async Task Get_AllUserProducts_Should_Be_NotNull()
        {
            var userProducts = new List<UserProduct>()
            {
                new UserProduct { UserId = "1", ProductId = 1 },
                new UserProduct { UserId = "2",ProductId = 2 }
            };

            var userProductGetResponses = new List<UserProductGetResponse>() 
            { 
                new UserProductGetResponse { ProductId = 1 , UsertId = "1"}, 
                new UserProductGetResponse {ProductId = 2, UsertId = "2" } 
            };
            
            _mapper.Map<IEnumerable<UserProductGetResponse>>(userProducts).Returns(userProductGetResponses);
            _userProductsRepository.GetUserProducts().Returns(userProducts);    

            var result = await _userProductService.GetAllUserProducts();

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(userProductGetResponses);
        }

        [Fact]
        public async Task Get_AllUserProducts_Should_Be_Empty()
        {
            var userProducts = new List<UserProduct>();
            var userProductGetResponses = new List<UserProductGetResponse>();

            _mapper.Map<IEnumerable<UserProductGetResponse>>(userProducts).Returns(userProductGetResponses);
            _userProductsRepository.GetUserProducts().Returns(userProducts);

            var result = await _userProductService.GetAllUserProducts();

            result.Should().NotBeNull();    
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task Get_UserProduct_Should_Be_NotNull()
        {
            var userProduct = new UserProduct { UserId = "1", ProductId = 1 };
            var userProductGetResponse = new UserProductGetResponse { ProductId = 1 , UsertId = "1"};

            _mapper.Map<UserProductGetResponse>(userProduct).Returns(userProductGetResponse);
            _userProductsRepository.GetUserProduct(userProduct.ProductId).Returns(userProduct);

            var result = await _userProductService.GetSpecificUserProduct(userProduct.ProductId);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(userProductGetResponse);
        }

        [Fact]
        public async Task Get_UserProduct_Should_Be_Null()
        {
            var userProduct = new UserProduct { UserId = "1", ProductId = 1 };
            var userProductGetResponse = new UserProductGetResponse { ProductId = 1, UsertId = "1" };

            _mapper.Map<UserProductGetResponse>(userProduct).Returns(userProductGetResponse);
            _userProductsRepository.GetUserProduct(userProduct.ProductId).Returns(Task.FromResult<UserProduct>(null));

            var result = await _userProductService.GetSpecificUserProduct(userProduct.ProductId);

            result.Should().BeNull();
        }

        [Fact]
        public async Task Update_UserProduct_Should_Be_NotNull()
        {
            var userProduct = new UserProduct { UserId = "1", ProductId = 1 };
            var userProductPostRequest = new UserProductPostRequest { ProductId = 1 };
            var userProductPostResponse = new UserProductPostResponse {  ProductId = 1 , UserId = "1"};

            _mapper.Map<UserProduct>(userProductPostRequest).Returns(userProduct);
            _userProductsRepository.UpdateUserProduct(userProduct.ProductId, userProduct).Returns(Task.FromResult<UserProduct>(userProduct));
            _mapper.Map<UserProductPostResponse>(userProduct).Returns(userProductPostResponse);

            var result = await _userProductService.UpdateUserProduct(userProduct.ProductId, userProductPostRequest);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(userProductPostResponse);

        }


        [Fact]
        public async Task Update_UserProduct_Should_Be_Null()
        {
            var userProduct = new UserProduct { UserId = "1", ProductId = 1 };
            var userProductPostRequest = new UserProductPostRequest { ProductId = 1 };

            _mapper.Map<UserProduct>(userProductPostRequest).Returns(userProduct);
            _userProductsRepository.UpdateUserProduct(userProduct.ProductId, userProduct).Returns(Task.FromResult<UserProduct>(null));

            var result = await _userProductService.UpdateUserProduct(userProduct.ProductId, userProductPostRequest);

            result.Should().BeNull();
        }

        [Fact]
        public async Task Delete_UserProduct_Should_Be_NotNull()
        {
            var userProduct = new UserProduct { UserId = "1", ProductId = 1 };
            var userProductDeleteResponse = new UserProductDeleteResponse { ProductId = 1 , UserId = "1"};

            _mapper.Map<UserProductDeleteResponse>(userProduct).Returns(userProductDeleteResponse);
            _userProductsRepository.DeleteUserProduct(userProduct.ProductId).Returns(Task.FromResult<UserProduct>(userProduct));

            var result = await _userProductService.DeleteUserProduct(userProduct.ProductId);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(userProductDeleteResponse);
        }

        [Fact]
        public async Task Delete_UserProduct_Should_Be_Null()
        {
            var userProduct = new UserProduct { UserId = "1", ProductId = 1 };
            var userProductDeleteResponse = new UserProductDeleteResponse { ProductId = 1, UserId = "1" };

            _mapper.Map<UserProductDeleteResponse>(userProduct).Returns(userProductDeleteResponse);
            _userProductsRepository.DeleteUserProduct(userProduct.ProductId).Returns(Task.FromResult<UserProduct>(null));

            var result = await _userProductService.DeleteUserProduct(userProduct.ProductId);

            result.Should().BeNull();
        }
    }
}
