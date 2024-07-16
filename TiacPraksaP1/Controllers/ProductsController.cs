using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;
using TiacPraksaP1.Data;
using TiacPraksaP1.DTOs.Post;
using TiacPraksaP1.Models;
using TiacPraksaP1.Services.Interfaces;
using TiacPraksaP1.Validators;

namespace TiacPraksaP1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ProductValidator _productValidator;

        public ProductsController(IProductService productService, ProductValidator  productValidator)
        {
            _productService = productService;
            _productValidator= productValidator;
        }

        //Create
        [HttpPost]
        public ActionResult<ProductPostResponse> AddProduct(ProductPostRequest product)
        {
            var result = _productValidator.Validate(product);
            if(result.IsValid)
            {
                var response = _productService.CreateProduct(product);
                if (response == null)
                {
                    return BadRequest("Adding went wrong.");
                }
                return Ok(response);
            }
            return BadRequest("Product fields were not valid" + result.ToString());
        }
        //Get 
        [HttpGet]
        public ActionResult<List<Product>> GetAllProducts()
        {
            var response = _productService.GetAllProducts();
            if (response == null)
            {
                return NotFound("No products were found");
            }
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public ActionResult<List<Product>> GetProduct(string name)
        {
            var response = _productService.GetSpecificProducts(name);
            if (response == null)
            {
                return NotFound("No products were found");
            }
            return Ok(response);
        }
        //Update
        [HttpPut]
        public ActionResult<List<ProductPostResponse>> UpdateProduct(ProductPostRequest UpdatedProduct)
        {
            var result = _productValidator.Validate(UpdatedProduct);
            if (result.IsValid)
            {
                var response = _productService.UpdateProduct(UpdatedProduct);
                if (response == null)
                {
                    return NotFound("No products were found");
                }
                return Ok(response);
            }
            return BadRequest("Product was not valid" + result.ToString());
        }
        

        
        //Delete
        [HttpDelete]
        public ActionResult<List<Product>> DeleteProduct(int id)
        {
            var response = _productService.DeleteProduct(id);
            if(response != null)
            {
                return Ok(response);
            }
            return NotFound("Product was not deleted. There was no product found.");
        }
    }
}
