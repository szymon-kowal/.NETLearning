using Entities;
using ServiceContract;
using ServiceContract.DTO;

namespace Services;

public class CountriesService : ICountriesService {
    private readonly List<Country> _countries;

    public CountriesService() {
        _countries = new List<Country>();
    }

    public CountryResponse AddCountry(CountryAddRequest? countryAddRequest) {
        if (countryAddRequest == null) {
            throw new ArgumentNullException(nameof(countryAddRequest));
        }

        if (countryAddRequest.CountryName == null) {
            throw new ArgumentNullException(nameof(countryAddRequest), "Country name can not be null");
        }

        if (_countries.Where(country => country.CountryName == countryAddRequest.CountryName).Any()) {
            throw new ArgumentException("Given country name already exists");
        }

        Country country = countryAddRequest.ToCountry();

        country.CountryID = Guid.NewGuid();

        _countries.Add(country);

        return country.ToCountryResponse();
    }

    public List<CountryResponse> GetAllCountries() {
        return _countries.Select(country => country.ToCountryResponse()).ToList();
    }

    public CountryResponse? GetCountryByCountryID(Guid? countryID) {
        if (countryID == null)
            return null;

        Country? countryResponseFromList = _countries.FirstOrDefault(temp => temp.CountryID == countryID);

        return countryResponseFromList?.ToCountryResponse();
    }
}