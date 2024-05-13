using System.ComponentModel.DataAnnotations;

namespace ServiceContract.DTO.Helpers {

	public class DateNotYoungerThanAttribute : ValidationAttribute {
		private readonly DateTime _dateNotYoungerThan;

		public DateNotYoungerThanAttribute(string dateNotYounger) {
			_dateNotYoungerThan = DateTime.Parse(dateNotYounger);
		}

		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
			if (value is DateTime dateTime) {
				if (dateTime < _dateNotYoungerThan) {
					return new ValidationResult($"The date must not be older than {_dateNotYoungerThan.ToShortDateString()}.");
				}
			}
			return ValidationResult.Success;
		}
	}
}