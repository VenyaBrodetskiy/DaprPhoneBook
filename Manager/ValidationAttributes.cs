using System.ComponentModel.DataAnnotations;

namespace Manager
{
    public class IsraeliPhoneNumberAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var phone = value as string;

            if (string.IsNullOrWhiteSpace(phone))
            {
                return new ValidationResult("Phone cannot be empty");
            }

            if (phone[0] == '+')
            {
                if (!phone[1..].All(char.IsDigit)) return new ValidationResult("Phone number should consist of digits only");
            }
            else
            {
                if (!phone.All(char.IsDigit)) return new ValidationResult("Phone number should consist of digits only");
            }

            if (!(phone.StartsWith("+972") || phone.StartsWith("0")))
            {
                return new ValidationResult("Phone number should start from +972 or 0");
            }

            if (phone.Length < 10 || phone.Length > 14)
            {
                return new ValidationResult("Phone lenght is invalid");
            }

            return ValidationResult.Success;
        }
    }
}
