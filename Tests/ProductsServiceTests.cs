using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NSubstitute;
using TiacPraksaP1.Controllers;
using TiacPraksaP1.DTOs.Delete;
using TiacPraksaP1.DTOs.Get;
using TiacPraksaP1.DTOs.Post;
using TiacPraksaP1.Models;
using TiacPraksaP1.Repositories.Interfaces;
using TiacPraksaP1.Services.Service;

namespace Tests
{
    public class ProductsServiceTests
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ProductService _productService;
        public ProductsServiceTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _productService = new ProductService(_productRepository,_mapper);
        }

        [Fact]
        public async Task Add_Product()
        {
            //Creating test DTOs
            var productRequest = new ProductPostRequest { Name = "Product1", Description = "Description1", Price = 100.0f };
            var product = new Product { Name = productRequest.Name, Description = productRequest.Description, Price = productRequest.Price };
            var productResponse = new ProductPostResponse { Name = "Product1", Description = "Description1", Price = 100.0f };

            _mapper.Map<Product>(productRequest).Returns(product);
            _productRepository.CreateProduct(product).Returns(Task.FromResult(product));
            _mapper.Map<ProductPostResponse>(product).Returns(productResponse);

            // Running method to create a product
            var result = await _productService.CreateProduct(productRequest);

            // Checking if the values are not null and if they match
            Assert.NotNull(result);
            Assert.Equal(productResponse, result);
        }
        [Fact]
        public async Task Add_Product_WhenNull_ReturnsNull()
        {
            // Act
            var result = await _productService.CreateProduct(null);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Get_Products()
        {
            var products = new List<Product>()
            {
                new Product {Id=1, Name = "Product1", Description = "Description1", Price = 100.0f },
                new Product {Id=2, Name = "Product2", Description = "Description2", Price = 100.0f }
            };
            //Creating test DTOs
            var productGetResponses = new List<ProductGetResponse>() 
            {
                new ProductGetResponse {Id=1, Name = "Product1", Description = "Description1", Price = 100.0f },
                new ProductGetResponse {Id=3, Name = "Product2", Description = "Description2", Price = 100.0f }
            };

            _productRepository.GetAllProducts().Returns(Task.FromResult((IEnumerable<Product>)products));
            _mapper.Map<List<ProductGetResponse>>(products).Returns(productGetResponses);

            // Running method to get all product
            var result = await _productService.GetAllProducts();

            // Checking if the values are not null and if they match
            Assert.NotNull(result);
            Assert.Equal(productGetResponses, result);
            Assert.True(productGetResponses.All(shouldItem => result.Any(isItem => isItem.Equals(shouldItem))));

        }

        [Fact]
        public async Task Update_Product()
        {
            // Creating test DTOs
            var productRequest = new ProductPostRequest { Id = 1, Description = "Updated Description", Name = "Updated Name", Price = 200.0f };
            var existingProduct = new Product { Id = 1, Name = "Old Name", Description = "Old Description", Price = 100.0f };
            var updatedProduct = new Product { Id = 1, Name = productRequest.Name, Description = productRequest.Description, Price = productRequest.Price };
            var productResponse = new ProductPostResponse { Id = 1, Name = productRequest.Name, Description = productRequest.Description, Price = productRequest.Price };

            _productRepository.GetSpecificProduct(productRequest.Id).Returns(Task.FromResult(existingProduct));
            _mapper.Map<Product>(productRequest).Returns(updatedProduct);
            _productRepository.UpdateProduct(updatedProduct).Returns(Task.FromResult(updatedProduct));
            _mapper.Map<ProductPostResponse>(updatedProduct).Returns(productResponse);

            // Running method to update a product
            var result = await _productService.UpdateProduct(productRequest);

            Console.WriteLine(result.Name);
            Console.WriteLine(productResponse.Name);
            // Checking if the values are not null and if they match
            Assert.NotNull(result);
            Assert.Equal(productResponse, result);
        }

        [Fact]
        public async Task Delete_Product_When_None_IsFound()
        {
            var productId = 1;
            _productRepository.GetSpecificProduct(productId).Returns(Task.FromResult<Product>(null));

            var result = await _productRepository.DeleteProduct(productId);
            Assert.Null(result);

        }

        [Fact]
        public async Task Delete_Product_When_IsFound()
        {
            var productId = 1;
            var product = new Product { Id = productId, Description = "Test", Name = "Test", Price = 10 };
            var productDeleteResponse = new ProductDeleteResponse { Id = productId , Description="Test" , Name="Test", Price=10};
            _productRepository.DeleteProduct(productId).Returns(product);
            _mapper.Map<Product>(productDeleteResponse).Returns(product);
            var result = await _productRepository.DeleteProduct(productId);

            Assert.NotNull(result);
            Assert.Equal(product, result);
        }
    }


}