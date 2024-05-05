using System.ComponentModel.DataAnnotations;

namespace EuroCarsUSA.Data.Attributes
{
    public class AtLeastOnePropertyAttribute : ValidationAttribute
    {
        private string[] PropertyList { get; set; }

        public AtLeastOnePropertyAttribute(params string[] propertyList)
        {
            this.PropertyList = propertyList;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            foreach (var propertyName in PropertyList)
            {
                var property = validationContext.ObjectType.GetProperty(propertyName);
                if (property == null)
                    return new ValidationResult(string.Format("Unknown property: {0}.", propertyName));

                var propertyValue = property.GetValue(validationContext.ObjectInstance, null);
                if (propertyValue != null && !string.IsNullOrEmpty(propertyValue.ToString()))
                    return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }

}
