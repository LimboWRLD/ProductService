using DataAccessLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using TiacPraksaP1.Data;
using TiacPraksaP1.Models;

namespace DataAccessLayer.Repository.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IConfiguration _configuration;

        public ProductRepository(AppDbContext productContext, IHttpContextAccessor httpContext, IConfiguration configuration)
        {
            _context = productContext;
            _httpContext = httpContext;
            _configuration = configuration;
        }

        private string GetUserId() => _httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public async Task<Product> UpdateProduct(Product product)
        {
            var oldProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
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
            var toDelete = _context.Products.FirstOrDefault(p => p.Id == ProuctId );
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
            var products = await _context.Products
                .Where(product => _context.UserProduts
                    .Any(up => up.UserId == GetUserId() && up.ProductId == product.Id))
                .ToListAsync();
            return products;
        }

        public async Task<Product> GetSpecificProduct(int ProductId)
        { 
            return await _context.Products
                                .FirstOrDefaultAsync(product => product.Id == ProductId && _context.UserProduts.Any(up => up.ProductId == ProductId && up.UserId == GetUserId()));

            // return await _context.Products.FirstOrDefaultAsync(x => x.Id == ProductId);
        }

        public async Task<Dictionary<string,string>> GetBasicStatistics()
        {
            var numberOfProducts = await _context.Products.CountAsync();    
            var averagePrice = await _context.Products.AverageAsync(x=> x.Price);
            var lowestPrice =await _context.Products.OrderBy(x => x.Price).Select(x=> x.Price).FirstOrDefaultAsync();
            var highestPrice = await _context.Products.OrderByDescending(x => x.Price).Select(x => x.Price).FirstOrDefaultAsync();
            var totalAssignedProducts = await _context.UserProduts.CountAsync();

            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("numberOfProducts", numberOfProducts.ToString());
            result.Add("averagePrice", averagePrice.ToString());
            result.Add("lowestPrice", lowestPrice.ToString());
            result.Add("highestPrice", highestPrice.ToString());
            result.Add("totalAssignedProducts", totalAssignedProducts.ToString());
            return result;
        }

        public async Task<IEnumerable<Dictionary<string, string>>> GetMostPopular(int? range)
        {
            var popularAssingedProducts = _context.UserProduts.GroupBy(x => x.ProductId).Select(x => new
            {
                productId = x.Key,
                count = x.Count()
            }).OrderByDescending(x => x.count).AsQueryable();

            var defaultRange = _configuration["ConstrainsApi:DefaultNumberOfPopular"];
            var numberOfElementsToTake = range.HasValue ? range.Value : int.Parse(defaultRange);

            popularAssingedProducts = popularAssingedProducts.Take(numberOfElementsToTake);
            

            var topProducts = new List<Dictionary<string,string>>();
            
            foreach (var product in popularAssingedProducts)
            {
                var dictionary = new Dictionary<string, string>();
                dictionary.Add("ProductId", product.productId.ToString());
                dictionary.Add("ProductName", await _context.Products.Where(x => x.Id == product.productId).Select(x => x.Name).FirstOrDefaultAsync());
                dictionary.Add("CountOfAssingments", product.count.ToString());
                dictionary.Add("Owner", await _context.Products.Where(x => x.Id == product.productId).Select(x => x.OwnerId).FirstOrDefaultAsync());
                topProducts.Add(dictionary);
            }
            return topProducts;
        }
       
    }
}
