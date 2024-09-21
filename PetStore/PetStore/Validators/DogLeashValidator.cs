using FluentValidation;
using PetStore.Models;

namespace PetStore.Validators
{
    public class DogLeashValidator : AbstractValidator<DogLeash>
    {
        public DogLeashValidator()
        {
            RuleFor(x => x.Name).NotEmpty();  // Name is required
            RuleFor(x => x.Price).GreaterThan(0);  // Price must be positive
            RuleFor(x => x.Quantity).GreaterThan(0);  // Quantity must be positive
            RuleFor(x => x.Description).MinimumLength(10).When(x => !string.IsNullOrEmpty(x.Description));  // Description must have at least 10 characters
        }
    }
}
