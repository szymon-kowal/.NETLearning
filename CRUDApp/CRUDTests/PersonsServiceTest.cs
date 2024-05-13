using ServiceContract;
using ServiceContract.DTO;
using Services;
using ServiceContract.Enums;
using Xunit.Abstractions;
using Entities;

namespace CRUDTests {

    public class PersonsServiceTest {
        private readonly IPersonsService _personService;
        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _outputHelper;

        public PersonsServiceTest(ITestOutputHelper testOutputHelper) {
            _personService = new PersonsService();
            _countriesService = new CountriesService();
            _outputHelper = testOutputHelper;
        }

        #region AddPerson

        [Fact]
        public void AddPerson_NullPerson() {
            //Arrange
            PersonAddRequest? request = null;
            //Act
            Assert.Throws<ArgumentNullException>(() => _personService.AddPerson(request));
        }

        [Fact]
        public void AddPerson_PersonNameIsNull() {
            PersonAddRequest? request = new PersonAddRequest() { PersonName = null };

            Assert.Throws<ArgumentException>(() => _personService.AddPerson(request));
        }

        [Fact]
        public void AddPerson_ProperPersonDetails() {
            PersonAddRequest? request = new PersonAddRequest() {
                PersonName = "John",
                Adress = "AdressTest",
                CountryID = Guid.NewGuid(),
                Email = "person@example.com",
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                RecevieNewsLetters = true,
            };
            PersonResponse person_response_from_add = _personService.AddPerson(request);
            List<PersonResponse> persons_list = _personService.GetAllPersons();

            Assert.True(person_response_from_add.PersonID != Guid.Empty);
            Assert.Contains(person_response_from_add, persons_list);
        }

        #endregion AddPerson

        #region GetPersonByPersonID

        [Fact]
        public void GetPersonByPersonID_NullPersonID() {
            Guid? personID = null;

            PersonResponse? person_response_from_get = _personService.GetPersonByPersonID(personID);

            Assert.Null(person_response_from_get);
        }

        [Fact]
        public void GetPersonByPersonID_WithValidPersonID() {
            //Arrange
            CountryAddRequest? country_request = new CountryAddRequest() { CountryName = "Canada" };
            CountryResponse country_response = _countriesService.AddCountry(country_request);
            //Act
            PersonAddRequest person_request = new PersonAddRequest() {
                PersonName = "John test",
                Adress = "AdressTest",
                CountryID = country_response.CountryId,
                Email = "person@example.com",
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                RecevieNewsLetters = false,
            };
            PersonResponse person_response_from_add = _personService.AddPerson(person_request);
            PersonResponse? person_response_from_get = _personService.GetPersonByPersonID(person_response_from_add.PersonID);
            //Act
            Assert.Equal(person_response_from_add, person_response_from_get);
        }

        #endregion GetPersonByPersonID

        #region GetAllPersons

        [Fact]
        public void GetAllPersons_EmptyList() {
            List<PersonResponse> persons_from_get = _personService.GetAllPersons();
            Assert.Empty(persons_from_get);
        }

        [Fact]
        public void GetAllPersons_AddFewPersons() {
            CountryAddRequest country_request_1 = new CountryAddRequest() {
                CountryName = "USA"
            };
            CountryAddRequest country_request_2 = new CountryAddRequest() {
                CountryName = "India"
            };

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_add_1 = new PersonAddRequest() {
                PersonName = "John test",
                Adress = "AdressTest",
                CountryID = country_response_1.CountryId,
                Email = "person1@example.com",
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                RecevieNewsLetters = false,
            };
            PersonAddRequest person_add_2 = new PersonAddRequest() {
                PersonName = "John test2",
                Adress = "AdressTest2",
                CountryID = country_response_2.CountryId,
                Email = "person2@example.com",
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                RecevieNewsLetters = false,
            };
            PersonAddRequest person_add_3 = new PersonAddRequest() {
                PersonName = "John test3",
                Adress = "AdressTest3",
                CountryID = country_response_2.CountryId,
                Email = "person3@example.com",
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                RecevieNewsLetters = true,
            };
            List<PersonAddRequest> person_requests = new List<PersonAddRequest>() {
                person_add_1, person_add_2, person_add_3
            };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest person_request in person_requests) {
                PersonResponse person_response = _personService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }

            _outputHelper.WriteLine("Expected : ");
            foreach (PersonResponse person_response_from_add in person_response_list_from_add) {
                _outputHelper.WriteLine(person_response_from_add.ToString());
            }

            List<PersonResponse> person_response_list_from_get = _personService.GetAllPersons();

            _outputHelper.WriteLine("Actual : ");
            foreach (PersonResponse person_response_from_get in person_response_list_from_get) {
                _outputHelper.WriteLine(person_response_from_get.ToString());
            }

            foreach (PersonResponse person_response_from_add in person_response_list_from_add) {
                Assert.Contains(person_response_from_add, person_response_list_from_get);
            }
        }

        #endregion GetAllPersons

        #region GetFilteredPersons

        [Fact]
        public void GetFilteredPersons_EmptyString() {
            CountryAddRequest country_request_1 = new CountryAddRequest() {
                CountryName = "USA"
            };
            CountryAddRequest country_request_2 = new CountryAddRequest() {
                CountryName = "India"
            };

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_add_1 = new PersonAddRequest() {
                PersonName = "John test",
                Adress = "AdressTest",
                CountryID = country_response_1.CountryId,
                Email = "person1@example.com",
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                RecevieNewsLetters = false,
            };
            PersonAddRequest person_add_2 = new PersonAddRequest() {
                PersonName = "John test2",
                Adress = "AdressTest2",
                CountryID = country_response_2.CountryId,
                Email = "person2@example.com",
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                RecevieNewsLetters = false,
            };
            PersonAddRequest person_add_3 = new PersonAddRequest() {
                PersonName = "John test3",
                Adress = "AdressTest3",
                CountryID = country_response_2.CountryId,
                Email = "person3@example.com",
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                RecevieNewsLetters = true,
            };
            List<PersonAddRequest> person_requests = new List<PersonAddRequest>() {
                person_add_1, person_add_2, person_add_3
            };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest person_request in person_requests) {
                PersonResponse person_response = _personService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }

            _outputHelper.WriteLine("Expected : ");
            foreach (PersonResponse person_response_from_add in person_response_list_from_add) {
                _outputHelper.WriteLine(person_response_from_add.ToString());
            }

            List<PersonResponse> person_response_list_from_search = _personService.GetFilteredPersons(nameof(Person.PersonName),
                "");

            _outputHelper.WriteLine("Actual : ");
            foreach (PersonResponse person_response_from_get in person_response_list_from_search) {
                _outputHelper.WriteLine(person_response_from_get.ToString());
            }

            foreach (PersonResponse person_response_from_add in person_response_list_from_add) {
                Assert.Contains(person_response_from_add, person_response_list_from_search);
            }
        }

        [Fact]
        public void GetFilteredPersons_ReturnsMatchingPersonWithValidData() {
            CountryAddRequest country_request_1 = new CountryAddRequest() {
                CountryName = "USA"
            };
            CountryAddRequest country_request_2 = new CountryAddRequest() {
                CountryName = "India"
            };

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_add_1 = new PersonAddRequest() {
                PersonName = "John test",
                Adress = "AdressTest",
                CountryID = country_response_1.CountryId,
                Email = "person1@example.com",
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                RecevieNewsLetters = false,
            };
            PersonAddRequest person_add_2 = new PersonAddRequest() {
                PersonName = "Mary",
                Adress = "AdressTest2",
                CountryID = country_response_2.CountryId,
                Email = "person2@example.com",
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                RecevieNewsLetters = false,
            };
            PersonAddRequest person_add_3 = new PersonAddRequest() {
                PersonName = "Rahman",
                Adress = "AdressTest3",
                CountryID = country_response_2.CountryId,
                Email = "person3@example.com",
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                RecevieNewsLetters = true,
            };

            List<PersonAddRequest> person_requests = new List<PersonAddRequest>() {
                person_add_1, person_add_2, person_add_3
            };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest person_request in person_requests) {
                PersonResponse person_response = _personService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }

            _outputHelper.WriteLine("Expected : ");
            foreach (PersonResponse person_response_from_add in person_response_list_from_add) {
                _outputHelper.WriteLine(person_response_from_add.ToString());
            }

            List<PersonResponse> person_response_list_from_search = _personService.GetFilteredPersons(nameof(Person.PersonName),
                "ma");

            _outputHelper.WriteLine("Actual : ");
            foreach (PersonResponse person_response_from_get in person_response_list_from_search) {
                _outputHelper.WriteLine(person_response_from_get.ToString());
            }

            foreach (PersonResponse person_response_from_add in person_response_list_from_add) {
                if (person_response_from_add.PersonName is not null) {
                    if (person_response_from_add.PersonName.Contains("ma", StringComparison.OrdinalIgnoreCase)) {
                        Assert.Contains(person_response_from_add, person_response_list_from_search);
                    }
                }
            }
        }

        #endregion GetFilteredPersons

        #region GetSortedPersons

        [Fact]
        public void GetSortedPersons_SortByPersonNameDescOrder() {
            CountryAddRequest country_request_1 = new CountryAddRequest() {
                CountryName = "USA"
            };
            CountryAddRequest country_request_2 = new CountryAddRequest() {
                CountryName = "India"
            };

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_add_1 = new PersonAddRequest() {
                PersonName = "John test",
                Adress = "AdressTest",
                CountryID = country_response_1.CountryId,
                Email = "person1@example.com",
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                RecevieNewsLetters = false,
            };
            PersonAddRequest person_add_2 = new PersonAddRequest() {
                PersonName = "Mary",
                Adress = "AdressTest2",
                CountryID = country_response_2.CountryId,
                Email = "person2@example.com",
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                RecevieNewsLetters = false,
            };
            PersonAddRequest person_add_3 = new PersonAddRequest() {
                PersonName = "Rahman",
                Adress = "AdressTest3",
                CountryID = country_response_2.CountryId,
                Email = "person3@example.com",
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                RecevieNewsLetters = true,
            };

            List<PersonAddRequest> person_requests = new List<PersonAddRequest>() {
                person_add_1, person_add_2, person_add_3
            };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest person_request in person_requests) {
                PersonResponse person_response = _personService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }

            _outputHelper.WriteLine("Expected : ");
            foreach (PersonResponse person_response_from_add in person_response_list_from_add) {
                _outputHelper.WriteLine(person_response_from_add.ToString());
            }

            List<PersonResponse> allPersonResponses = _personService.GetAllPersons();
            List<PersonResponse> person_response_list_from_sort = _personService.GetSortedPersons(allPersonResponses, nameof(Person.PersonName), SortOrderOptions.DESC);

            _outputHelper.WriteLine("Actual : ");
            foreach (PersonResponse person_response_from_get in person_response_list_from_sort) {
                _outputHelper.WriteLine(person_response_from_get.ToString());
            }

            person_response_list_from_add = person_response_list_from_add.OrderByDescending(x => x.PersonName).ToList();

            for (int i = 0;i < person_response_list_from_add.Count;i++) {
                Assert.Equal(person_response_list_from_add[i], person_response_list_from_sort[i]);
            }
        }

        #endregion GetSortedPersons

        #region UpdatePerson

        [Fact]
        public void UpdatePerson_ThrowArguNullExOnNullPerson() {
            PersonUpdateRequest? person_update_request = null;

            Assert.Throws<ArgumentNullException>(() => {
                _personService.UpdatePerson(person_update_request);
            }
            );
        }

        [Fact]
        public void UpdatePerson_InputInvalidPerson() {
            PersonUpdateRequest? person_update_request = new PersonUpdateRequest() {
                PersonID = Guid.NewGuid()
            };
            Assert.Throws<ArgumentException>(() => {
                _personService.UpdatePerson(person_update_request);
            });
        }

        [Fact]
        public void UpdatePerson_PersonNameIsNull() {
            CountryAddRequest country_add_request = new CountryAddRequest() { CountryName = "UK" };

            CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

            PersonAddRequest person_add_request = new PersonAddRequest() {
                PersonName = "John",
                Adress = "AdressTest",
                CountryID = country_response_from_add.CountryId,
                Email = "person@example.com",
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                RecevieNewsLetters = true,
            };

            PersonResponse person_response_from_add = _personService.AddPerson(person_add_request);

            PersonUpdateRequest person_update_request = person_response_from_add.ToPersonUpdateRequest();
            person_update_request.PersonName = null;

            Assert.Throws<ArgumentException>(() => {
                _personService.UpdatePerson(person_update_request);
            });
        }

        [Fact]
        public void UpdatePerson_ValidInput() {
            CountryAddRequest country_add_request = new CountryAddRequest() { CountryName = "UK" };

            CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

            PersonAddRequest person_add_request = new PersonAddRequest() {
                PersonName = "John",
                Adress = "AdressTest",
                CountryID = country_response_from_add.CountryId,
                Email = "person@example.com",
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                RecevieNewsLetters = true,
            };

            PersonResponse person_response_from_add = _personService.AddPerson(person_add_request);

            PersonUpdateRequest person_update_request = person_response_from_add.ToPersonUpdateRequest();

            person_update_request.PersonName = "Adam";
            person_update_request.Adress = "New Adress";

            PersonResponse person_response_from_update = _personService.UpdatePerson(person_update_request);

            PersonResponse? person_response_from_get = _personService.GetPersonByPersonID(person_response_from_update.PersonID);

            Assert.Equal(person_response_from_get, person_response_from_update);
        }

        #endregion UpdatePerson

        #region DeletePerson

        [Fact]
        public void DeletePerson_ValidPersonID() {
            CountryAddRequest country_add_request = new CountryAddRequest() { CountryName = "UK" };

            CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

            PersonAddRequest person_add_request = new PersonAddRequest() {
                PersonName = "John",
                Adress = "AdressTest",
                CountryID = country_response_from_add.CountryId,
                Email = "person@example.com",
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                RecevieNewsLetters = true,
            };

            PersonResponse person_response_from_add = _personService.AddPerson(person_add_request);

            bool isDeleted = _personService.DeletePerson(person_response_from_add.PersonID);
            Assert.True(isDeleted);
        }

        [Fact]
        public void DeletePerson_InvalidPersonID() {
            CountryAddRequest country_add_request = new CountryAddRequest() { CountryName = "UK" };

            CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

            PersonAddRequest person_add_request = new PersonAddRequest() {
                PersonName = "John",
                Adress = "AdressTest",
                CountryID = country_response_from_add.CountryId,
                Email = "person@example.com",
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                RecevieNewsLetters = true,
            };

            PersonResponse person_response_from_add = _personService.AddPerson(person_add_request);

            bool isDeleted = _personService.DeletePerson(Guid.NewGuid());

            Assert.False(isDeleted);
        }

        #endregion DeletePerson
    }
}