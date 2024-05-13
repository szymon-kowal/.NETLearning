using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services.FinnhubService;
using Services.StockService;

namespace StockAppWithXUnit.Controllers {

	public class TradeController : Controller {
		private readonly IConfiguration _configuration;
		private readonly IFinnHubService _finnHubService;
		private readonly IStocksService _stocksService;
		private readonly TradingOptions _tradeOptions;

		public TradeController(IOptions<TradingOptions> tradingOptions, IConfiguration configuration, IFinnHubService finnHubService, IStocksService stocksService) {
			_configuration = configuration;
			_finnHubService = finnHubService;
			_stocksService = stocksService;
			_tradeOptions = tradingOptions.Value;
		}

		[Route("/")]
		public IActionResult Index() {
			Dictionary<string, object>? companyProfileDic = _finnHubService.GetCompanyProfile(_tradeOptions.DefaultStockSymbol);
			return View();
		}
	}
}