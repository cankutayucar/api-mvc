using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.Core.Dtos;
using FluentValidation;

namespace Architecture.Service.Validators
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("{propertyName} is rquired")
                .NotEmpty().WithMessage("{propertyName} is rquired");
            RuleFor(x => x.Price).InclusiveBetween(1, int.MaxValue).WithMessage("{propertyName} must be greater 0");
            RuleFor(x => x.Stock).InclusiveBetween(1, int.MaxValue).WithMessage("{propertyName} must be greater 0");
            RuleFor(x => x.CategoryId).InclusiveBetween(1, int.MaxValue).WithMessage("{propertyName} must be greater 0");
        }
    }
}
