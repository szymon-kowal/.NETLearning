using ServiceContract.DTO;
using ServiceContract;
using Entities;
using Services.Helpers;
using ServiceContract.Enums;

namespace Services {

    public class PersonsService : IPersonsService {
        private readonly List<Person> _persons;
        private readonly ICountriesService _countriesService;

        public PersonsService() {
            _persons = new List<Person>();
            _countriesService = new CountriesService();
        }

        private PersonResponse ConvertPersonToPersonResponse(Person person) {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countriesService.GetCountryByCountryID(person.CountryID)?.CountryName;
            return personResponse;
        }

        public PersonResponse AddPerson(PersonAddRequest? personAddRequest) {
            if (personAddRequest == null)
                throw new ArgumentNullException(nameof(personAddRequest));

            //Validation

            ValidationHelper.ModelValidation(personAddRequest);

            Person personObj = personAddRequest.ToPerson();

            personObj.PersonID = Guid.NewGuid();

            _persons.Add(personObj);

            return ConvertPersonToPersonResponse(personObj);
        }

        public List<PersonResponse> GetAllPersons() {
            return _persons.Select(item => item.ToPersonResponse()).ToList();
        }

        public PersonResponse? GetPersonByPersonID(Guid? personID) {
            if (personID is null)
                return null;
            Person? person = _persons.FirstOrDefault(temp => temp.PersonID == personID);
            if (person == null)
                return null;
            return person.ToPersonResponse();
        }

        public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString) {
            List<PersonResponse> allPersons = GetAllPersons();
            List<PersonResponse> matchingPersons = allPersons;

            if (string.IsNullOrEmpty(searchString) || string.IsNullOrEmpty(searchString))
                return allPersons;

            switch (searchBy) {
                case nameof(Person.PersonName):
                    matchingPersons = allPersons.Where(personRes => (string.IsNullOrEmpty(personRes.PersonName) || personRes.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase))).ToList();
                    break;

                case nameof(Person.Email):
                    matchingPersons = allPersons.Where(personRes => (string.IsNullOrEmpty(personRes.Email) || personRes.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase))).ToList();
                    break;

                case nameof(Person.DateOfBirth):
                    matchingPersons = allPersons.Where(personRes => (personRes.DateOfBirth != null) ? personRes.DateOfBirth.Value.ToString("dd MM yyyy").Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(Person.Gender):
                    matchingPersons = allPersons.Where(personRes => (!string.IsNullOrEmpty(personRes.Gender)) ? personRes.Gender.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(Person.CountryID):
                    matchingPersons = allPersons.Where(personRes => (!string.IsNullOrEmpty(personRes.Country)) ? personRes.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(Person.Adress):
                    matchingPersons = allPersons.Where(personRes => (!string.IsNullOrEmpty(personRes.Adress)) ? personRes.Adress.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                default:
                    matchingPersons = allPersons;
                    break;
            }

            return matchingPersons;
        }

        public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder) {
            if (string.IsNullOrEmpty(sortBy))
                return allPersons;
            List<PersonResponse> sortedPersons = (sortBy, sortOrder)
                switch {
                    (nameof(PersonResponse.PersonName), SortOrderOptions.ASC) => allPersons.OrderBy(personRes => personRes.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),
                    (nameof(PersonResponse.PersonName), SortOrderOptions.DESC) => allPersons.OrderByDescending(personRes => personRes.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),
                    (nameof(PersonResponse.Email), SortOrderOptions.ASC) => allPersons.OrderBy(personRes => personRes.Email, StringComparer.OrdinalIgnoreCase).ToList(),
                    (nameof(PersonResponse.Email), SortOrderOptions.DESC) => allPersons.OrderByDescending(personRes => personRes.Email, StringComparer.OrdinalIgnoreCase).ToList(),
                    (nameof(PersonResponse.DateOfBirth), SortOrderOptions.ASC) => allPersons.OrderBy(personRes => personRes.DateOfBirth).ToList(),
                    (nameof(PersonResponse.DateOfBirth), SortOrderOptions.DESC) => allPersons.OrderByDescending(personRes => personRes.DateOfBirth).ToList(),
                    (nameof(PersonResponse.Age), SortOrderOptions.ASC) => allPersons.OrderBy(personRes => personRes.Age).ToList(),
                    (nameof(PersonResponse.Age), SortOrderOptions.DESC) => allPersons.OrderByDescending(personRes => personRes.Age).ToList(),
                    (nameof(PersonResponse.Gender), SortOrderOptions.ASC) => allPersons.OrderBy(personRes => personRes.Gender, StringComparer.OrdinalIgnoreCase).ToList(),
                    (nameof(PersonResponse.Gender), SortOrderOptions.DESC) => allPersons.OrderByDescending(personRes => personRes.Gender, StringComparer.OrdinalIgnoreCase).ToList(),
                    (nameof(PersonResponse.Country), SortOrderOptions.ASC) => allPersons.OrderBy(personRes => personRes.Country, StringComparer.OrdinalIgnoreCase).ToList(),
                    (nameof(PersonResponse.Country), SortOrderOptions.DESC) => allPersons.OrderByDescending(personRes => personRes.Country, StringComparer.OrdinalIgnoreCase).ToList(),
                    (nameof(PersonResponse.Adress), SortOrderOptions.ASC) => allPersons.OrderBy(personRes => personRes.Adress, StringComparer.OrdinalIgnoreCase).ToList(),
                    (nameof(PersonResponse.Adress), SortOrderOptions.DESC) => allPersons.OrderByDescending(personRes => personRes.Adress, StringComparer.OrdinalIgnoreCase).ToList(),
                    (nameof(PersonResponse.RecevieNewsLetters), SortOrderOptions.ASC) => allPersons.OrderBy(personRes => personRes.RecevieNewsLetters).ToList(),
                    (nameof(PersonResponse.RecevieNewsLetters), SortOrderOptions.DESC) => allPersons.OrderByDescending(personRes => personRes.RecevieNewsLetters).ToList(),
                    _ => allPersons,
                };
            return sortedPersons;
        }

        public PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest) {
            if (personUpdateRequest == null)
                throw new ArgumentNullException(nameof(personUpdateRequest));

            ValidationHelper.ModelValidation(personUpdateRequest);

            Person? matchingPerson = _persons.FirstOrDefault(person => person.PersonID == personUpdateRequest.PersonID) ?? throw new ArgumentException("Given person ID does not exist");

            matchingPerson.PersonName = personUpdateRequest.PersonName;
            matchingPerson.Email = personUpdateRequest.Email;
            matchingPerson.Adress = personUpdateRequest.Adress;
            matchingPerson.Gender = personUpdateRequest.Gender.ToString();
            matchingPerson.CountryID = personUpdateRequest.CountryID;
            matchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
            matchingPerson.PersonID = personUpdateRequest.PersonID;
            matchingPerson.RecevieNewsLetters = personUpdateRequest.RecevieNewsLetters;

            return matchingPerson.ToPersonResponse();
        }

        public bool DeletePerson(Guid? personID) {
            if (personID == null)
                throw new ArgumentNullException("Person ID should not be null");

            Person? personToDelete = _persons.FirstOrDefault(person => person.PersonID == personID);

            if (personToDelete == null)
                return false;

            _persons.RemoveAll(temp => temp.PersonID == personID);
            return true;
        }
    }
}