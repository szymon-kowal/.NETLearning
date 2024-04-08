using ServiceInterface;

namespace ServiceProvider {

	public class WeatherProvider : IWeatherService {

		public CityWeather? GetWeatherByCityCode(string CityCode) {
			return GetWeatherDetails().Where(city => city.CityUniqueCode == CityCode).FirstOrDefault();
		}

		public List<CityWeather> GetWeatherDetails() {
			return new List<CityWeather>
				{
					new CityWeather { CityUniqueCode = "LDN", CityName = "London", DateAndTime = DateTime.Parse("2030-01-01 8:00"), TemperatureFahrenheit = 33 },
					new CityWeather { CityUniqueCode = "NYC", CityName = "New York", DateAndTime = DateTime.Parse("2030-01-01 3:00"), TemperatureFahrenheit = 60 }, // Corrected CityName based on CityUniqueCode
					new CityWeather { CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = DateTime.Parse("2030-01-01 9:00"), TemperatureFahrenheit = 82 }
				};
		}
	}
}