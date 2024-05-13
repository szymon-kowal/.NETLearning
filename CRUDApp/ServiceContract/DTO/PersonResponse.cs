using Entities;
using ServiceContract.DTO;
using ServiceContract.Enums;
using System.Runtime.CompilerServices;

namespace ServiceContract.DTO {

    /// <summary>
    /// Represents DTO class that is used as return type of most methods of Person Service
    /// </summary>
    public class PersonResponse {

        public Guid PersonID {
            get; set;
        }

        public string? PersonName {
            get; set;
        }

        public string? Email {
            get; set;
        }

        public DateTime? DateOfBirth {
            get; set;
        }

        public string? Gender {
            get; set;
        }

        public Guid? CountryID {
            get; set;
        }

        public string? Country {
            get; set;
        }

        public string? Adress {
            get; set;
        }

        public bool RecevieNewsLetters {
            get; set;
        }

        public double? Age {
            get; set;
        }

        /// <summary>
        /// Compares current object data with the parameter object
        /// </summary>
        /// <param name="obj">The PersonResponse Object to compare</param>
        /// <returns>True or false, indicating whether all person details are matched with specific paramtere object</returns>
        public override bool Equals(object? obj) {
            if (obj == null)
                return false;
            if (obj.GetType() != typeof(PersonResponse))
                return false;
            PersonResponse personRes = (PersonResponse) obj;
            return PersonID == personRes.PersonID && PersonName == personRes.PersonName
                && Email == personRes.Email && DateOfBirth == personRes.DateOfBirth
                && Gender == personRes.Gender && CountryID == personRes.CountryID
                && Adress == personRes.Adress && RecevieNewsLetters == personRes.RecevieNewsLetters;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override string ToString() {
            return $"Person ID: {PersonID}, Person Name : {PersonName}, " +
                $"Email : {Email}, Date of birth : {DateOfBirth?.ToString("dd MM yyyy")}," +
                $"Gender : {Gender}, CountryID : {CountryID}, Country : {Country}, " +
                $"Adress : {Adress},  Receive News Letters : {RecevieNewsLetters}";
        }

        public PersonUpdateRequest ToPersonUpdateRequest() {
            return new PersonUpdateRequest() {
                PersonID = PersonID,
                PersonName = PersonName,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Gender = (GenderOptions) Enum.Parse(typeof(GenderOptions), Gender, true),
                CountryID = CountryID,
                Adress = Adress,
                RecevieNewsLetters = RecevieNewsLetters
            };
            ;
        }
    }
}

public static class PersonExtensions {

    /// <summary>
    /// An extension method to convert an object of Person class into PersonResponse class
    /// </summary>
    /// <param name="person">The Person object to response</param>
    /// <returns>Converted PersonResponse object</returns>
    public static PersonResponse ToPersonResponse(this Person person) {
        return new PersonResponse() {
            PersonID = person.PersonID,
            PersonName = person.PersonName,
            Email = person.Email,
            DateOfBirth = person.DateOfBirth,
            RecevieNewsLetters = person.RecevieNewsLetters,
            Gender = person.Gender,
            Adress = person.Adress,
            Age = (person.DateOfBirth is not null) ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25) : null,
        };
    }
}