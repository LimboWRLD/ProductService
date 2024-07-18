using FluentValidation;
using TiacPraksaP1.DTOs.Post;
using TiacPraksaP1.Models;
namespace TiacPraksaP1.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator() {
            RuleFor(product  => product.Name).NotEmpty();
            RuleFor(product => product.Description).NotEmpty();
            RuleFor(product => product.Price).NotEmpty();
        }
    }
}
