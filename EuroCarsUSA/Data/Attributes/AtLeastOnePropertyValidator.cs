using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Localization;

namespace EuroCarsUSA.Data.Attributes
{
    public class AtLeastOnePropertyValidator : AttributeAdapterBase<AtLeastOnePropertyAttribute>
    {
        public AtLeastOnePropertyValidator(AtLeastOnePropertyAttribute attribute, IStringLocalizer stringLocalizer)
            : base(attribute, stringLocalizer)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-atleastoneproperty", GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-atleastoneproperty-properties", string.Join(",", Attribute._propertyNames));
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return Attribute.ErrorMessage;
        }
    }

    public class AtLeastOnePropertyValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider _baseProvider = new ValidationAttributeAdapterProvider();

        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if (attribute is AtLeastOnePropertyAttribute atleastOnePropertyAttribute)
            {
                return new AtLeastOnePropertyValidator(atleastOnePropertyAttribute, stringLocalizer);
            }

            return _baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}
