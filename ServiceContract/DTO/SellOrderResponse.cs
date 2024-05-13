using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract.DTO {

	public class SellOrderResponse {

		public Guid SellOrderID {
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
			if (obj == null || obj is not SellOrderResponse input) {
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

	public static class SellOrderExtension {

		public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder) {
			return new SellOrderResponse() {
				SellOrderID = sellOrder.SellOrderID,
				StockSymbol = sellOrder.StockSymbol,
				StockName = sellOrder.StockName,
				DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
				Quantity = sellOrder.Quantity,
				Price = sellOrder.Price,
				TradeAmount = sellOrder.Price * sellOrder.Quantity
			};
		}
	}
}