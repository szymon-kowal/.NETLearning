using Microsoft.AspNetCore.Mvc;
using WeatherAppLayoutAssignment.Models;

namespace WeatherAppLayoutAssignment.Controllers {

	public class HomeController : Controller {

		[Route("/")]
		public IActionResult Index() {
			List<CityWeather> dataList = CreateList();
			return View(dataList);
		}

		private List<CityWeather> CreateList() {
			List<CityWeather> list = new List<CityWeather>() {
				new() {CityUniqueCode = "LDN",
					CityName = "London",
					DateAndTime = DateTime.Parse("2030-01-01 8:00"),
					TemperatureFahrenheit = 33
				},
				new() {
					CityUniqueCode = "NYC",
					CityName = "New York",
					DateAndTime = DateTime.Parse("2030-01-01 3:00"),
					TemperatureFahrenheit = 60
				},
				new() {
					CityUniqueCode = "PAR",
					CityName = "Paris",
					DateAndTime = DateTime.Parse("2030-01-01 9:00"),
					TemperatureFahrenheit = 82
				}
			};
			return list;
		}
	}
}