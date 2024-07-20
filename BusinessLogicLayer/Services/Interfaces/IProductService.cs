using TiacPraksaP1.DTOs.Delete;
using TiacPraksaP1.DTOs.Get;
using TiacPraksaP1.DTOs.Post;
using TiacPraksaP1.Models;

namespace TiacPraksaP1.Services.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductGetResponse>> GetAllProducts();

        public Task<ProductGetResponse> GetSpecificProduct(int id);

        public Task<ProductPostResponse> CreateProduct(ProductPostRequest product);

        public Task<ProductDeleteResponse> DeleteProduct(int id);

        public Task<ProductPostResponse> UpdateProduct(int id,ProductPostRequest product);
    }
}
