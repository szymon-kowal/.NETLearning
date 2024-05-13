using ServiceContract.DTO;

namespace ServiceContract {

    /// <summary>
    /// Represents business logic for manipulating Country entity
    /// </summary>
    public interface ICountriesService {

        /// <summary>
        /// Adds a country object to the list of countries
        /// </summary>
        /// <param name="countryAddRequest"></param>
        /// <returns>Returns the country object after adding it (including newly generated country id)</returns>
        CountryResponse AddCountry(CountryAddRequest? countryAddRequest);

        /// <summary>
        /// Returns all countries from list of countries
        /// </summary>
        /// <returns>All countries from list as List<Countries></returns>
        List<CountryResponse> GetAllCountries();

        /// <summary>
        /// Returns a country object based on the given country ID
        /// </summary>
        /// <param name="countryID"></param>
        /// <returns>Matching country as CountryResponse object</returns>
        CountryResponse? GetCountryByCountryID(Guid? countryID);
    }
}