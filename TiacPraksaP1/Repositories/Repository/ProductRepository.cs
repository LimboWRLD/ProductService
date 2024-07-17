using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using TiacPraksaP1.Data;
using TiacPraksaP1.Models;
using TiacPraksaP1.Repositories.Interfaces;
using TiacPraksaP1.Validators;

namespace TiacPraksaP1.Repositories.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;


        public ProductRepository(AppDbContext productContext)
        {
            this._context = productContext;
        }

        public Product UpdateProduct(Product product)
        {
            if (product != null)
            {
                
                    var oldProduct = _context.Products.FirstOrDefault(p => p.Id == product.Id);
                    if (oldProduct != null)
                    {
                        _context.Products.Remove(oldProduct);
                        _context.Products.Add(product);
                        _context.SaveChanges();
                        return product;
                    }
                    return null;
            }
            return null;
        }

        public Product CreateProduct(Product product)
        {
            if (product != null)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return product;
            }
            return null;
        }

        public Product DeleteProduct(int id)
        {
            if (!(id == null))
            {
                Product toDelete = _context.Products.FirstOrDefault(p => p.Id == id);
                if(toDelete != null)
                {
                    _context.Remove(toDelete);
                    _context.SaveChanges();
                    return toDelete;
                }
                throw new NullReferenceException("Product that you wanted to delete is null");
            }
            throw new ArgumentException("The id you searched for is null or empty");
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetSpecificProduct(int id)
        {
            if (id != null)
            {
                var products = GetAllProducts();


                foreach (var product in products)
                {
                    if (product.Id == id)
                    {
                        return product;
                    }
                }
      
            }
            throw new ArgumentNullException("Id was null");

        }
    }
}
