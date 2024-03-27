using Microsoft.AspNetCore.Mvc;
using WeatherAppAssignment.Models;

namespace WeatherAppAssignment.Controllers {

    public class HomeController : Controller {

        [Route("/")]
        public IActionResult Index() {
            var list = CreateList();
            return View(list);
        }

        [Route("weather/{cityCode}")]
        public IActionResult SingleCity(string? cityCode) {
            if (cityCode == null) {
                return View("Error");
            }
            CityWeather? selectedList = CreateList().FirstOrDefault(city => city.CityUniqueCode == cityCode);
            if (selectedList == null) {
                return View("Error");
            }
            return View("SingleCity", selectedList);
        }

        private IList<CityWeather> CreateList() {
            IList<CityWeather> list = new List<CityWeather>() {
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