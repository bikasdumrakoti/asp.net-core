using System.ComponentModel.DataAnnotations;

namespace ECommerceOrdersApp.CustomValidators
{
    public class MinimumYearValidatorAttribute : ValidationAttribute
    {
        public string MinimumOrderDate { get; set; }
        public MinimumYearValidatorAttribute(string minimumOrderDate)
        {
            MinimumOrderDate = minimumOrderDate;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime orderDate = (DateTime)value;
                DateTime minimumOrderDate = Convert.ToDateTime(MinimumOrderDate);
                if (orderDate < minimumOrderDate)
                {
                    return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName, MinimumOrderDate));
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
