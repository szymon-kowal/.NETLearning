namespace ServiceInterface {

    public interface IWeatherService {

        List<CityWeather>? GetWeatherDetails();

        CityWeather? GetWeatherByCityCode(string CityCode);
    }
}