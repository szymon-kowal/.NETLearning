using Microsoft.AspNetCore.Mvc;
using ServiceInterface;

namespace WeatherAppWithDI.Controllers {

    public class HomeController : Controller {
        private readonly IWeatherService _weatherService;

        public HomeController(IWeatherService weatherService) {
            _weatherService = weatherService;
        }

        [Route("/")]
        public IActionResult Index() {
            List<CityWeather>? listOfWeathers = _weatherService.GetWeatherDetails();
            if (listOfWeathers != null && listOfWeathers.Count == 0) {
                return View(listOfWeathers);
            }
            return View("Error");
        }

        [Route("weather/{cityCode}")]
        public IActionResult SingleCityWeather(string? cityCode) {
            if (!String.IsNullOrEmpty(cityCode)) {
                CityWeather? cityWeather = _weatherService.GetWeatherByCityCode(cityCode);
                if (cityWeather is not null) {
                    return View("_SingleCityWeatherPartial", cityWeather);
                }
            }
            return View("Error");
        }
    }
}