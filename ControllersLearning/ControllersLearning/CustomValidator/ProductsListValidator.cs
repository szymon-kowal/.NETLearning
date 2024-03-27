using ControllersLearning.Models;
using System.ComponentModel.DataAnnotations;

namespace ControllersLearning.CustomValidator {

    public class ProductsListValidator : ValidationAttribute {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
            if (value != null) {
                List<Product> val = (List<Product>) value;
                if (val.Count > 0) {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("Order should have at least one product", new string[] { nameof(validationContext.MemberName) });
        }
    }
}