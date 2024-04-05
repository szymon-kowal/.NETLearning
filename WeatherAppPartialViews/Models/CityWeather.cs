namespace WeatherAppPartialViews.Models;

public class CityWeather {

	public string CityUniqeCode {
		get; set;
	} = string.Empty;

	public string CityName {
		get; set;
	} = string.Empty;

	public DateTime DateAndTime {
		get; set;
	}

	public int TemperatureFahrenheit {
		get; set;
	}
}