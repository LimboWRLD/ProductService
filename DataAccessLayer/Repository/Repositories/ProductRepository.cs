using DataAccessLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TiacPraksaP1.Data;
using TiacPraksaP1.Models;

namespace DataAccessLayer.Repository.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public ProductRepository(AppDbContext productContext, IHttpContextAccessor httpContext)
        {
            _context = productContext;
            _httpContext = httpContext;
        }

        private string GetUserId() => _httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public async Task<Product> UpdateProduct(Product product)
        {
            var oldProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id && p.OwnerId == GetUserId());
            if (oldProduct != null)
            {
                oldProduct.Name = product.Name;
                oldProduct.Description = product.Description;
                oldProduct.Price = product.Price;
                await _context.SaveChangesAsync();
                return product;
            }
            return oldProduct;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            var userId = GetUserId();
            if (product != null)
            {
                product.OwnerId=userId;
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product;
            }
            return product;
        }

        public async Task<Product> DeleteProduct(int ProuctId)
        {
            var userId = GetUserId();
            var toDelete = _context.Products.FirstOrDefault(p => p.Id == ProuctId && p.OwnerId == userId);
            if (toDelete != null)
            {
                _context.Products.Remove(toDelete);
                await _context.SaveChangesAsync();
                return toDelete;
            }
            return toDelete;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var userProducts =await _context.UserProduts.Where(x => x.UserId == GetUserId()).ToListAsync();
            return await _context.Products
                                .Where(product => userProducts.Any(up=> up.ProductId == product.Id)).ToListAsync();
        }

        public async Task<Product> GetSpecificProduct(int ProductId)
        { 
            return await _context.Products
                                .FirstOrDefaultAsync(product => product.Id == ProductId && _context.UserProduts.Any(up => up.ProductId == ProductId && up.UserId == GetUserId()));

            // return await _context.Products.FirstOrDefaultAsync(x => x.Id == ProductId);
        }
    }
}
