using Microsoft.AspNetCore.Mvc;
using WeatherAppViewComponents.Models;

namespace WeatherAppViewComponents.ViewComponents {

	public class CityViewComponent : ViewComponent {

		public async Task<IViewComponentResult> InvokeAsync(CityWeather cityWeather) {
			return View(cityWeather); //invokes view of the view component at Views/Shared/Components/City/Sample.cshtml
		}
	}
}