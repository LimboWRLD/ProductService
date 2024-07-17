using FluentValidation;
using TiacPraksaP1.DTOs.Post;
using TiacPraksaP1.Models;

namespace TiacPraksaP1.Validators
{
    public class RoleValidator:AbstractValidator<RolePostRequest>
    {
        public RoleValidator() {
            RuleFor(role => role.Name).NotEmpty();
        }
    }
}
