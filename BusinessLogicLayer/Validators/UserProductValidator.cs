using DataAccessLayer.Entities;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Validators
{
    public class UserProductValidator : AbstractValidator<UserProduct>
    {
        public UserProductValidator() 
        {
            RuleFor(userProduct=> userProduct.ProductId).NotEmpty();
            RuleFor(userProduct=> userProduct.UserId).NotEmpty();
        }

    }
}
