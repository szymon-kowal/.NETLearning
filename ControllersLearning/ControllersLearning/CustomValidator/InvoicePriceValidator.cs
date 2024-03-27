using ControllersLearning.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ControllersLearning.CustomValidator {

    public class InvoicePriceValidator : ValidationAttribute {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
            if (value != null) {
                double val = (double) value;
                PropertyInfo? propertyInfo = validationContext.ObjectType.GetProperty(nameof(Order.Products));
                if (propertyInfo != null) {
                    List<Product> listOfProducts = (List<Product>) propertyInfo.GetValue(validationContext.ObjectInstance)!;
                    if (listOfProducts != null) {
                        double productsValueSum = listOfProducts.Sum(x => x.Price * x.Quantity);
                        if (productsValueSum == val) {
                            return ValidationResult.Success;
                        }
                    }
                }
            }
            return null;
        }
    }
}