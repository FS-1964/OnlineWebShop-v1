using System;
using System.ComponentModel.DataAnnotations;

namespace WebShop.DAL.Auth
{
    public class DateValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)

        {

            DateTime MinDate = new DateTime(1900, 1, 1);

            string Message = string.Empty;

            if (Convert.ToDateTime(value) < MinDate)

            {

                Message = "Birth Date cannot be less than 1900.mm.dd";

                return new ValidationResult(Message);

            }

            return ValidationResult.Success;

        }
    }
}
