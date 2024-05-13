using ServiceContract.DTO;

namespace Services.StockService {

	public interface IStocksService {

		BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest);

		SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest);

		List<BuyOrderResponse> GetBuyOrders();

		List<SellOrderResponse> GetSellOrders();
	}
}