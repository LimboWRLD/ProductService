using TiacPraksaP1.DTOs.Delete;
using TiacPraksaP1.DTOs.Get;
using TiacPraksaP1.DTOs.Post;
using TiacPraksaP1.Models;

namespace TiacPraksaP1.Services.Interfaces
{
    public interface IProductService
    {
        public List<ProductGetResponse> GetAllProducts();

        public List<ProductGetResponse> GetSpecificProducts(string name);

        public ProductPostResponse CreateProduct(ProductPostRequest product);

        public ProductDeleteResponse DeleteProduct(int id);

        public ProductPostResponse UpdateProduct(ProductPostRequest product);
    }
}
