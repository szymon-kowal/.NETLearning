using ServiceContract.DTO;
using ServiceContract.Enums;

namespace ServiceContract;

/// <summary>
/// Represents business logic for manipulating Person entity
/// </summary>
public interface IPersonsService {

    /// <summary>
    /// Adds a new person into the list of persons
    /// </summary>
    /// <param name="personAddRequest"></param>
    /// <returns>Same person details with generated PersonID</returns>
    PersonResponse AddPerson(PersonAddRequest? personAddRequest);

    /// <summary>
    /// Returns all persons
    /// </summary>
    /// <returns>Returns a List of PersonResponses</returns>
    List<PersonResponse> GetAllPersons();

    /// <summary>
    /// Returns person based on the given Id.
    /// </summary>
    /// <param name="personID"></param>
    /// <returns>Matching person object</returns>
    PersonResponse? GetPersonByPersonID(Guid? personID);

    /// <summary>
    /// Returns all person objects that matches with the given search field and search string
    /// </summary>
    /// <param name="searchBy">Search field to search</param>
    /// <param name="searchString">Search strong to search</param>
    /// <returns>A list of PersonResponses matching input params</returns>
    List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString);

    /// <summary>
    /// Returns sorted list of persons
    /// </summary>
    /// <param name="allPersons">Represents list of persons to sort</param>
    /// <param name="sortBy">Name of the property (key), based on which the persons should be sorted</param>
    /// <param name="sortOrder">ASC or DESC</param>
    /// <returns>Returns sorted persons as PersonResponse list</returns>
    List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder);

    /// <summary>
    /// Updates given Person data based on on given person ID;
    /// </summary>
    /// <param name="personUpdateRequest">Person details to update</param>
    /// <returns>Returns The person response object after update</returns>
    PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest);

    /// <summary>
    /// Deletes a person based on the given person id.
    /// </summary>
    /// <param name="personID">PersonID to delete</param>
    /// <returns>Returns true, if the deletion is successfull, otherwise return false</returns>
    bool DeletePerson(Guid? personID);
}