using ServiceContract;
using Entities;
using Services;
using ServiceContract.DTO;
using Xunit;

namespace CRUDTests {

    public class CountriesServiceTest {
        private readonly ICountriesService _countriesService;

        public CountriesServiceTest() {
            _countriesService = new CountriesService();
        }

        #region AddCountry

        // When CountryAddRequest is null, it should throw ArgumentNullException
        [Fact]
        public void AddCountry_NullCountry() {
            // Arrange
            CountryAddRequest? request = null;
            //Assert
            Assert.Throws<ArgumentNullException>(() => {
                //Act
                _countriesService.AddCountry(request);
            });
        }

        // When CountryName is null, it should throw ArgumentException
        [Fact]
        public void AddCountry_CountryNameIsNull() {
            // Arrange
            CountryAddRequest? request = new CountryAddRequest() { CountryName = null };
            //Assert
            Assert.Throws<ArgumentNullException>(() => {
                //Act
                _countriesService.AddCountry(request);
            });
        }

        // When CountryName is duplicate, it should throw ArgumentException
        [Fact]
        public void AddCountry_DuplicateCountryName() {
            // Arrange
            CountryAddRequest? request1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest? request2 = new CountryAddRequest() { CountryName = "USA" };
            //Assert
            Assert.Throws<ArgumentException>(() => {
                //Act
                _countriesService.AddCountry(request1);
                _countriesService.AddCountry(request2);
            });
        }

        // When you supply country name, it should insert (add) the country to the existing list of countries
        [Fact]
        public void AddCountry_ProperCountryDetails() {
            // Arrange
            CountryAddRequest? request = new CountryAddRequest() { CountryName = "Japan" };
            //Assert
            CountryResponse response = _countriesService.AddCountry(request);
            List<CountryResponse> countries_from_GetAllCountries = _countriesService.GetAllCountries();
            //Act
            Assert.True(response.CountryId != Guid.Empty);
            Assert.Contains(response, countries_from_GetAllCountries);
        }

        #endregion AddCountry

        #region GetAllCountries

        [Fact]
        public void GetAllCountries_EmptyList() {
            //Act
            List<CountryResponse> countryResponsesList = _countriesService.GetAllCountries();
            //Assert
            Assert.Empty(countryResponsesList);
        }

        [Fact]
        public void GetAllCountries_AddFewCountries() {
            List<CountryAddRequest> country_add_request = new List<CountryAddRequest>() {
                new CountryAddRequest() { CountryName = "USA" },
                new CountryAddRequest() { CountryName = "Japan" }
            };

            List<CountryResponse> countries_list_from_add_country = new();

            foreach (CountryAddRequest country_request in country_add_request) {
                countries_list_from_add_country.Add(_countriesService.AddCountry(country_request));
            }

            List<CountryResponse> actualCountryResponseList = _countriesService.GetAllCountries();

            foreach (CountryResponse expected_country in countries_list_from_add_country) {
                Assert.Contains(expected_country, actualCountryResponseList);
            }
        }

        #endregion GetAllCountries

        #region GetCountryByCountryID

        [Fact]
        public void GetCountryByCountryID_NullCountryID() {
            //Arrange
            Guid? countryID = null;
            //Act
            CountryResponse? country_response_from_get_method = _countriesService.GetCountryByCountryID(countryID);
            //Assert
            Assert.Null(country_response_from_get_method);
        }

        [Fact]
        public void GetCountryByCountryID_ValidCountryID() {
            //Arrange
            CountryAddRequest? country_add_request = new CountryAddRequest() { CountryName = "Japan" };
            CountryResponse? country_response_from_add = _countriesService.AddCountry(country_add_request);
            //Act
            CountryResponse? country_response_from_get = _countriesService.GetCountryByCountryID(country_response_from_add.CountryId);
            //Assert
            Assert.Equal(country_response_from_add, country_response_from_get);
        }

        #endregion GetCountryByCountryID
    }
}