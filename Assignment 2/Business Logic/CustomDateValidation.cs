using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Assignment_2.Business_Logic
{
    public class CustomDateValidation : ValidationAttribute
    {
        public CustomDateValidation()
            : base("{0} contains invalid character.")
        {
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dt = Convert.ToDateTime(value);
            if (value != null)
            {
                if (dt.CompareTo(DateTime.Today)>0)
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}