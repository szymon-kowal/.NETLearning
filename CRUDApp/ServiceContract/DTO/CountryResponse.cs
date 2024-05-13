using Entities;

namespace ServiceContract.DTO {

    /// <summary>
    /// DTO class that is used as return type for most of CountriesService methods
    /// </summary>
    public class CountryResponse {

        public Guid CountryId {
            get; set;
        }

        public string? CountryName {
            get; set;
        }

        public override bool Equals(object? obj) {
            if (obj == null)
                return false;

            if (obj is CountryResponse otherCountryResponse)
                return this.CountryId == otherCountryResponse.CountryId && this.CountryName == otherCountryResponse.CountryName;

            return false;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }

    public static class CountryExtenstions {

        public static CountryResponse ToCountryResponse(this Country country) {
            return new CountryResponse() {
                CountryId = country.CountryID,
                CountryName = country.CountryName
            };
        }
    }
}