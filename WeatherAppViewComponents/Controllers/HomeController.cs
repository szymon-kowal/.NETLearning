using Microsoft.AspNetCore.Mvc;
using WeatherAppViewComponents.Models;

namespace WeatherAppViewComponents.Controllers;

public class HomeController : Controller {

	[Route("/")]
	public IActionResult Index() {
		List<CityWeather> listOfCityWeathers = GetCityWeatherList();
		return View(listOfCityWeathers);
	}

	[Route("weather/{cityCode}")]
	public IActionResult About(string? cityCode) {
		if (String.IsNullOrEmpty(cityCode)) {
			return View("Error");
		}
		CityWeather? city = GetCityWeatherList().Where(temp => temp.CityUniqueCode == cityCode).FirstOrDefault();
		return View("WeatherSingleCity", city);
	}

	private List<CityWeather> GetCityWeatherList() {
		return new List<CityWeather> {
			new CityWeather {
				CityUniqueCode = "LDN",
				CityName = "London",
				DateAndTime = new DateTime(2030, 01, 01, 8, 0, 0),
				TemperatureFahrenheit = 33
			},
			new CityWeather {
				CityUniqueCode = "NYC",
				CityName = "New York",
				DateAndTime = new DateTime(2030, 01, 01, 3, 0, 0),
				TemperatureFahrenheit = 60
			},
			new CityWeather {
				CityUniqueCode = "PAR",
				CityName = "Paris",
				DateAndTime = new DateTime(2030, 01, 01, 9, 0, 0),
				TemperatureFahrenheit = 82
			}
		};
	}
}