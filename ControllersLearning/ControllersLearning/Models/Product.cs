using System.ComponentModel.DataAnnotations;

namespace ControllersLearning.Models;

public class Product {

    [Display(Name = "Product code")]
    public int ProductCode {
        get; set;
    }

    [Display(Name = "Product price")]
    [Range(0.01, 10000, ErrorMessage = @"{0} should be between {1} and {2}")]
    public double Price {
        get; set;
    }

    [Display(Name = "Product quantity")]
    [Range(1, 10000, ErrorMessage = @"{0} should be between {1} and {2}")]
    public int Quantity {
        get; set;
    }
}