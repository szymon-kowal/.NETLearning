using Microsoft.AspNetCore.Mvc;
using WeatherAppPartialViews.Models;

namespace WeatherAppPartialViews.Controllers;

public class HomeController : Controller {

	[Route("/")]
	public IActionResult Index() {
		List<CityWeather> cityWeatherList = GetCityWeatherList();
		return View(cityWeatherList);
	}

	[Route("weather/{cityCode}")]
	public IActionResult SingleCity(string? cityCode) {
		if (cityCode != null) {
			List<CityWeather> cityWeatherList = GetCityWeatherList();
			CityWeather? cityWeather = cityWeatherList.FirstOrDefault(city => city.CityUniqeCode == cityCode);
			if (cityWeather != null) {
				return View(cityWeather);
			}
		}
		return View("Error");
	}

	private List<CityWeather> GetCityWeatherList() {
		return new List<CityWeather> {
			new CityWeather {
				CityUniqeCode = "LDN",
				CityName = "London",
				DateAndTime = new DateTime(2030, 01, 01, 8, 0, 0),
				TemperatureFahrenheit = 33
			},
			new CityWeather {
				CityUniqeCode = "NYC",
				CityName = "New York",
				DateAndTime = new DateTime(2030, 01, 01, 3, 0, 0),
				TemperatureFahrenheit = 60
			},
			new CityWeather {
				CityUniqeCode = "PAR",
				CityName = "Paris",
				DateAndTime = new DateTime(2030, 01, 01, 9, 0, 0),
				TemperatureFahrenheit = 82
			}
		};
	}
}