using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiacPraksaP1.DTOs.Post;
using TiacPraksaP1.Models;
using TiacPraksaP1.Services.Interfaces;
using TiacPraksaP1.Validators;

namespace TiacPraksaP1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ProductValidator _productValidator;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, ProductValidator productValidator, IMapper mapper)
        {
            _productService = productService;
            _productValidator = productValidator;
            _mapper = mapper;
        }

        //Create
        [HttpPost]
        public async Task<ActionResult<ProductPostResponse>> AddProduct(ProductPostRequest product)
        {
            var result = _productValidator.Validate(_mapper.Map<Product>(product));
            if(result.IsValid)
            {
                var response = await _productService.CreateProduct(product);
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
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var response =await _productService.GetAllProducts();
            if (response == null)
            {
                return NotFound("No products were found");
            }
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var response =await _productService.GetSpecificProduct(id);
            if (response == null)
            {
                return NotFound("No products were found");
            }
            return Ok(response);
        }
        //Update
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<ProductPostResponse>>> UpdateProduct(ProductPostRequest UpdatedProduct)
        {
            var result = _productValidator.Validate(_mapper.Map<Product>(UpdatedProduct));
            if (result.IsValid)
            {
                var response =await _productService.UpdateProduct(UpdatedProduct);
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Product>>> DeleteProduct(int id)
        {
            var response = await _productService.DeleteProduct(id);
            if(response != null)
            {
                return Ok(response);
            }
            return NotFound("Product was not deleted. There was no product found.");
        }
    }
}
