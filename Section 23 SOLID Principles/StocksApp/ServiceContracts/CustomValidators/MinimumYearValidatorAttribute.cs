using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.CustomValidators
{
    public class MinimumYearValidatorAttribute : ValidationAttribute
    {
        public DateTime MinimumDate { get; set; } = DateTime.Parse("2000-01-01");

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date = (DateTime)value;
                if (date < MinimumDate)
                {
                    return new ValidationResult($"Date should not be older than {MinimumDate} !");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            return null;
        }
    }
}
