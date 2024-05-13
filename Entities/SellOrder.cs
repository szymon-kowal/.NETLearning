using System.ComponentModel.DataAnnotations;

namespace Entities {

    public class SellOrder {

        public Guid SellOrderID {
            get; set;
        }

        [Required(ErrorMessage = "Stock symbol is mandatory")]
        public string? StockSymbol {
            get; set;
        }

        [Required(ErrorMessage = "Stock name is mandatory")]
        public string? StockName {
            get; set;
        }

        public DateTime DateAndTimeOfOrder {
            get; set;
        }

        [Required]
        [Range(1, 100000)]
        public uint Quantity {
            get; set;
        }

        [Required]
        [Range(1, 10000)]
        public double Price {
            get; set;
        }
    }
}