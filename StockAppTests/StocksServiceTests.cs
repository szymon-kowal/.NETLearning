using Entities;
using ServiceContract.DTO;
using Services.StockService;

namespace StockAppTests {

	public class StocksServiceTests {
		private readonly StocksService _stocksService;

		public StocksServiceTests() {
			_stocksService = new StocksService();
		}

		#region CreateBuyOrder()

		[Fact]
		public void CreateBuyOrder_NullOrderReq_ThrowArgNullEx() {
			BuyOrderRequest? request = null;

			Assert.Throws<ArgumentNullException>(() => _stocksService.CreateBuyOrder(request));
		}

		[Fact]
		public void CreateBuyOrder_StockSymbolNull_ThrowArgEx() {
			BuyOrderRequest buy_request = new BuyOrderRequest() {
				StockSymbol = null,
				DateAndTimeOfOrder = DateTime.Now,
				Price = 10,
				Quantity = 10,
				StockName = "AAPL"
			};

			Assert.Throws<ArgumentException>(() => _stocksService.CreateBuyOrder(buy_request));
		}

		[Fact]
		public void CreateBuyOrder_StockNameNull_ThrowArgEx() {
			BuyOrderRequest buy_request = new BuyOrderRequest() {
				StockSymbol = "EXA",
				DateAndTimeOfOrder = DateTime.Now,
				Price = 10,
				Quantity = 10,
				StockName = null
			};

			Assert.Throws<ArgumentException>(() => _stocksService.CreateBuyOrder(buy_request));
		}

		[Fact]
		public void CreateBuyOrder_DateOlderThan2000_ThrowArgEx() {
			BuyOrderRequest buy_request = new BuyOrderRequest() {
				StockSymbol = "EXA",
				DateAndTimeOfOrder = new DateTime(1999, 1, 1),
				Price = 10,
				Quantity = 10,
				StockName = "AAPL"
			};

			Assert.Throws<ArgumentException>(() => _stocksService.CreateBuyOrder(buy_request));
		}

		[Fact]
		public void CreateBuyOrder_Quantity0_ThrowArgEx() {
			BuyOrderRequest buy_request = new BuyOrderRequest() {
				StockSymbol = "EXA",
				DateAndTimeOfOrder = DateTime.Now,
				Price = 10,
				Quantity = 0,
				StockName = "AAPL"
			};

			Assert.Throws<ArgumentException>(() => _stocksService.CreateBuyOrder(buy_request));
		}

		[Fact]
		public void CreateBuyOrder_Quantity100001_ThrowArgEx() {
			BuyOrderRequest buy_request = new BuyOrderRequest() {
				StockSymbol = "EXA",
				DateAndTimeOfOrder = DateTime.Now,
				Price = 10,
				Quantity = 100001,
				StockName = "AAPL"
			};

			Assert.Throws<ArgumentException>(() => _stocksService.CreateBuyOrder(buy_request));
		}

		[Fact]
		public void CreateBuyOrder_Price0_ThrowArgEx() {
			BuyOrderRequest? buy_request = new BuyOrderRequest() {
				StockSymbol = "EXA",
				DateAndTimeOfOrder = DateTime.Now,
				Price = 0,
				Quantity = 10,
				StockName = "AAPL"
			};

			Assert.Throws<ArgumentException>(() => _stocksService.CreateBuyOrder(buy_request));
		}

		[Fact]
		public void CreateBuyOrder_ValidInput_BuyOrderResponse() {
			BuyOrderRequest buy_request = new BuyOrderRequest() {
				StockSymbol = "EXA",
				DateAndTimeOfOrder = DateTime.Now,
				Price = 1,
				Quantity = 10,
				StockName = "AAPL"
			};

			BuyOrderResponse buy_response_from_add = _stocksService.CreateBuyOrder(buy_request);

			Assert.NotEqual(Guid.Empty, buy_response_from_add.BuyOrderID);
		}

		#endregion CreateBuyOrder()

		#region CreateSellOrder()

		[Fact]
		public void CreateSellOrder_NullOrderReq_ThrowArgNullEx() {
			SellOrderRequest? sell_request = null;

			Assert.Throws<ArgumentNullException>(() => _stocksService.CreateSellOrder(sell_request));
		}

		[Fact]
		public void CreateSellOrder_StockSymbolNull_ThrowArgEx() {
			SellOrderRequest sell_request = new SellOrderRequest() {
				StockSymbol = null,
				DateAndTimeOfOrder = DateTime.Now,
				Price = 10,
				Quantity = 10,
				StockName = "AAPL"
			};

			Assert.Throws<ArgumentException>(() => _stocksService.CreateSellOrder(sell_request));
		}

		[Fact]
		public void CreateSellOrder_StockNameNull_ThrowArgEx() {
			SellOrderRequest sell_request = new SellOrderRequest() {
				StockSymbol = "EXA",
				DateAndTimeOfOrder = DateTime.Now,
				Price = 10,
				Quantity = 10,
				StockName = null
			};

			Assert.Throws<ArgumentException>(() => _stocksService.CreateSellOrder(sell_request));
		}

		[Fact]
		public void CreateSellOrder_DateOlderThan2000_ThrowArgEx() {
			SellOrderRequest sell_request = new SellOrderRequest() {
				StockSymbol = "EXA",
				DateAndTimeOfOrder = new DateTime(1999, 1, 1),
				Price = 10,
				Quantity = 10,
				StockName = "AAPL"
			};

			Assert.Throws<ArgumentException>(() => _stocksService.CreateSellOrder(sell_request));
		}

		[Fact]
		public void CreateSellOrder_Quantity0_ThrowArgEx() {
			SellOrderRequest sell_request = new SellOrderRequest() {
				StockSymbol = "EXA",
				DateAndTimeOfOrder = DateTime.Now,
				Price = 10,
				Quantity = 0,
				StockName = "AAPL"
			};

			Assert.Throws<ArgumentException>(() => _stocksService.CreateSellOrder(sell_request));
		}

		[Fact]
		public void CreateSellOrder_Quantity100001_ThrowArgEx() {
			SellOrderRequest sell_request = new SellOrderRequest() {
				StockSymbol = "EXA",
				DateAndTimeOfOrder = DateTime.Now,
				Price = 10,
				Quantity = 100001,
				StockName = "AAPL"
			};

			Assert.Throws<ArgumentException>(() => _stocksService.CreateSellOrder(sell_request));
		}

		[Fact]
		public void CreateSellOrder_Price0_ThrowArgEx() {
			SellOrderRequest? sell_request = new SellOrderRequest() {
				StockSymbol = "EXA",
				DateAndTimeOfOrder = DateTime.Now,
				Price = 0,
				Quantity = 10,
				StockName = "AAPL"
			};

			Assert.Throws<ArgumentException>(() => _stocksService.CreateSellOrder(sell_request));
		}

		[Fact]
		public void CreateSellOrder_ValidInput_BuyOrderResponse() {
			SellOrderRequest sell_request = new SellOrderRequest() {
				StockSymbol = "EXA",
				DateAndTimeOfOrder = DateTime.Now,
				Price = 1,
				Quantity = 10,
				StockName = "AAPL"
			};

			SellOrderResponse sell_response_from_add = _stocksService.CreateSellOrder(sell_request);

			Assert.NotEqual(Guid.Empty, sell_response_from_add.SellOrderID);
		}

		#endregion CreateSellOrder()

		#region GetBuyOrders()

		[Fact]
		public void GetBuyOrders_DefaultList_IsEmpty() {
			List<BuyOrderResponse> buy_order_res = _stocksService.GetBuyOrders();

			Assert.Empty(buy_order_res);
		}

		[Fact]
		public void GetBuyOrders_WithCoupleInputs_ToBeValid() {
			BuyOrderRequest buy_request1 = new BuyOrderRequest() {
				StockSymbol = "EXA",
				DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00"),
				Price = 10,
				Quantity = 10,
				StockName = "smth"
			};
			BuyOrderRequest buy_request2 = new BuyOrderRequest() {
				StockSymbol = "EXA2",
				DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00"),
				Price = 102,
				Quantity = 102,
				StockName = "smth"
			};
			BuyOrderRequest buy_request3 = new BuyOrderRequest() {
				StockSymbol = "EXA3",
				DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00"),
				Price = 103,
				Quantity = 103,
				StockName = "smth"
			};

			List<BuyOrderRequest> req_list = new() {
				buy_request1,buy_request2, buy_request3
			};

			List<BuyOrderResponse> buy_order_res_from_add = new();

			foreach (var item in req_list) {
				buy_order_res_from_add.Add(_stocksService.CreateBuyOrder(item));
			}

			List<BuyOrderResponse> buy_order_res_from_get = _stocksService.GetBuyOrders();

			foreach (BuyOrderResponse buy_order_res in buy_order_res_from_get) {
				Assert.Contains(buy_order_res, buy_order_res_from_add);
			}
		}

		#endregion GetBuyOrders()

		#region GetSellOrders()

		[Fact]
		public void GetSellOrders_() {
		}

		#endregion GetSellOrders()
	}
}