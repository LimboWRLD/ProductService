using FluentValidation;
using TiacPraksaP1.DTOs.Post;

namespace TiacPraksaP1.Validators
{
    public class UserValidator:AbstractValidator<UserPostRequest>
    {
        public UserValidator() 
        {
            RuleFor(user=> user.UserName).NotEmpty();
            RuleFor(user=>user.Email).NotEmpty();
            RuleFor(user=>user.Password).NotEmpty();
        }
    }
}
