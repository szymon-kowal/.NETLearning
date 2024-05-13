using Entities;
using ServiceContract.DTO;
using ServiceContract.DTO.Helpers;

namespace Services.StockService {

	public class StocksService : IStocksService {
		private readonly List<BuyOrder> _buyOrders;
		private readonly List<SellOrder> _sellOrders;

		public StocksService() {
			_buyOrders = new List<BuyOrder>();
			_sellOrders = new List<SellOrder>();
		}

		public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest) {
			if (buyOrderRequest == null)
				throw new ArgumentNullException(nameof(buyOrderRequest));

			ValidationHelper.ModelValidation(buyOrderRequest);

			BuyOrder buyOrder = buyOrderRequest.ConvertToBuyOrder();

			buyOrder.BuyOrderID = Guid.NewGuid();

			_buyOrders.Add(buyOrder);

			return buyOrder.ToBuyOrderResponse();
		}

		public SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest) {
			if (sellOrderRequest == null)
				throw new ArgumentNullException(nameof(sellOrderRequest));

			ValidationHelper.ModelValidation(sellOrderRequest);

			SellOrder sellOrder = sellOrderRequest.ConvertToSellOrder();

			sellOrder.SellOrderID = Guid.NewGuid();

			_sellOrders.Add(sellOrder);

			return sellOrder.ToSellOrderResponse();
		}

		public List<BuyOrderResponse> GetBuyOrders() {
			return _buyOrders
				.OrderByDescending(item => item.DateAndTimeOfOrder)
				.Select(item => item.ToBuyOrderResponse())
				.ToList();
		}

		public List<SellOrderResponse> GetSellOrders() {
			return _sellOrders
				.OrderByDescending(item => item.DateAndTimeOfOrder)
				.Select(item => item.ToSellOrderResponse())
				.ToList();
			;
		}
	}
}