using System.ComponentModel.DataAnnotations;
using ControllersLearning.CustomValidator;

namespace ControllersLearning.Models;

public class Order {

    [Required(ErrorMessage = @"{0} can not be empty")]
    public int OrderNo {
        get; set;
    }

    [Required(ErrorMessage = @"{0} can not be empty")]
    public DateTime OrderDate {
        get; set;
    }

    [InvoicePriceValidator]
    [Required(ErrorMessage = @"{0} can not be empty")]
    public double InvoicePrice {
        get; set;
    }

    [ProductsListValidator(ErrorMessage = @"{0} can not be empty")]
    [Display(Name = "Products")]
    public List<Product?> Products {
        get; set;
    } = [];
}