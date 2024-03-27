using ECommerceOrdersApp.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ECommerceOrdersApp.CustomValidators
{
    public class InvoicePriceValidatorAttribute : ValidationAttribute
    {
        public string OtherPropertyName { get; set; }

        public InvoicePriceValidatorAttribute(string otherPropertyName)
        {
            OtherPropertyName = otherPropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                //get Invoice Price
                double invoicePrice = Convert.ToDouble(value);

                //get Products
                PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(OtherPropertyName);

                if (otherProperty != null)
                {
                    var products = otherProperty.GetValue(validationContext.ObjectInstance) as IList<Product>;
                    if (products != null)
                    {
                        double? totalCost = 0.0;
                        foreach (var product in products)
                        {
                            totalCost += product.Price * product.Quantity;
                        }
                        if (totalCost != invoicePrice)
                        {
                            //return new ValidationResult(ErrorMessage, new string[] { validationContext.MemberName });
                            return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName));
                        }
                        else
                        {
                            return ValidationResult.Success;
                        }
                    }
                    return null;
                }
                return null;
            }
            return null;
        }
    }
}
