using System;
using System.ComponentModel.DataAnnotations;

public class FutureDateAttribute : ValidationAttribute
{

    // Validates that the provided date is today or in the future.
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime dateValue)
        {
            DateTime today = DateTime.Today; // Removes time part
            if (dateValue < today) // Compare only the date part
            {
                return new ValidationResult(ErrorMessage ?? "The date must be today or in the future.");
            }
        }

        return ValidationResult.Success;
    }
}
