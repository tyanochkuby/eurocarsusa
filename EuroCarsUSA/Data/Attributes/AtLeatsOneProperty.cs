using System.ComponentModel.DataAnnotations;

namespace EuroCarsUSA.Data.Attributes
{
    public class AtLeastOnePropertyAttribute : ValidationAttribute
    {
        public readonly string[] _propertyNames;

        public AtLeastOnePropertyAttribute(params string[] propertyNames)
        {
            _propertyNames = propertyNames;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var properties = validationContext.ObjectType.GetProperties()
                .Where(p => _propertyNames.Contains(p.Name))
                .Select(p => p.GetValue(validationContext.ObjectInstance, null))
                .ToList();

            if (properties.Any(p => p != null && !string.IsNullOrEmpty(p.ToString())))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
