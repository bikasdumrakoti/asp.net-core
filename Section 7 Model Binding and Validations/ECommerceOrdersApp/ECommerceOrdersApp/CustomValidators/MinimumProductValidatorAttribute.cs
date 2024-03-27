using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace ECommerceOrdersApp.CustomValidators
{
    public class MinimumProductValidatorAttribute : ValidationAttribute
    {
        public int MinimumProduct { get; set; }
        public MinimumProductValidatorAttribute(int minimumProduct)
        {
            MinimumProduct = minimumProduct;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var products = value as IList;
                if (products.Count < MinimumProduct)
                {
                    return new ValidationResult(string.Format(ErrorMessage, MinimumProduct));
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
