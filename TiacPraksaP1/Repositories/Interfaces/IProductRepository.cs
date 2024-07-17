using TiacPraksaP1.Models;

namespace TiacPraksaP1.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public Product CreateProduct(Product product);

        public Product UpdateProduct(Product product);

        public Product DeleteProduct(int id);

        public IEnumerable<Product> GetAllProducts();

        public Product GetSpecificProduct(int id);
    }
}
