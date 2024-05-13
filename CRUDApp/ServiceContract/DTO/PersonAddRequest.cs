using Entities;
using ServiceContract.Enums;
using System.ComponentModel.DataAnnotations;

namespace ServiceContract.DTO;

/// <summary>
/// Acts as DTO for inserting a new person
/// </summary>
public class PersonAddRequest {

    [Required(ErrorMessage = "Person Name can not be blank")]
    public string? PersonName {
        get; set;
    }

    [Required(ErrorMessage = "Person Email can not be blank")]
    [EmailAddress(ErrorMessage = "Email should be valid email")]
    public string? Email {
        get; set;
    }

    public DateTime? DateOfBirth {
        get; set;
    }

    public GenderOptions? Gender {
        get; set;
    }

    public Guid? CountryID {
        get; set;
    }

    public string? Adress {
        get; set;
    }

    public bool RecevieNewsLetters {
        get; set;
    }

    /// <summary>
    /// Converts current object of PersonAddRequest into a new object of Person
    /// </summary>
    /// <returns></returns>
    public Person ToPerson() {
        return new Person {
            PersonName = PersonName,
            Email = Email,
            DateOfBirth = DateOfBirth,
            Gender = Gender.ToString(),
            CountryID = CountryID,
            Adress = Adress,
            RecevieNewsLetters = RecevieNewsLetters
        };
    }
}