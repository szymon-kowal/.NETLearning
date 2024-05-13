using Entities;
using ServiceContract.DTO.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ServiceContract.DTO {

	public class BuyOrderRequest {

		[Required(ErrorMessage = "Stock symbol is mandatory")]
		public string? StockSymbol {
			get; set;
		}

		[Required(ErrorMessage = "Stock name is mandatory")]
		public string? StockName {
			get; set;
		}

		[Required]
		[DateNotYoungerThan("2000, 1, 1")]
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

		public BuyOrder ConvertToBuyOrder() {
			return new BuyOrder() {
				StockSymbol = StockSymbol,
				StockName = StockName,
				Price = Price,
				DateAndTimeOfOrder = DateAndTimeOfOrder,
				Quantity = Quantity,
			};
		}
	}
}