using Entities;

namespace ServiceContract.DTO {

	public class BuyOrderResponse {

		public Guid BuyOrderID {
			get; set;
		}

		public string? StockSymbol {
			get; set;
		}

		public string? StockName {
			get; set;
		}

		public DateTime DateAndTimeOfOrder {
			get; set;
		}

		public uint Quantity {
			get; set;
		}

		public double Price {
			get; set;
		}

		public double TradeAmount {
			get; set;
		}

		public override bool Equals(object? obj) {
			if (obj == null || obj is not BuyOrderResponse input) {
				return false;
			}
			return StockSymbol == input.StockSymbol
				&& StockName == input.StockName
				&& Price == input.Price
				&& DateAndTimeOfOrder == input.DateAndTimeOfOrder
				&& Quantity == input.Quantity
				&& Price == input.Price;
		}

		public override int GetHashCode() {
			return StockSymbol!.GetHashCode();
		}
	}

	public static class BuyOrderExtension {

		public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder) {
			return new BuyOrderResponse() {
				BuyOrderID = buyOrder.BuyOrderID,
				StockSymbol = buyOrder.StockSymbol,
				StockName = buyOrder.StockName,
				DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
				Quantity = buyOrder.Quantity,
				Price = buyOrder.Price,
				TradeAmount = buyOrder.Price * buyOrder.Quantity
			};
		}
	}
}