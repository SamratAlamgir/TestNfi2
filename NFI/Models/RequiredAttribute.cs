using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace NFI.Models
{
    public class RequiredAttribute : System.ComponentModel.DataAnnotations.RequiredAttribute, IClientValidatable
    {
        private string _displayName;

        public RequiredAttribute(string language = "nok")
        {
            ErrorMessage = language == "nok" ? "Dette feltet er påkrevd." : "This field is required.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var attributes =
                validationContext.ObjectType.GetProperty(validationContext.MemberName)
                    .GetCustomAttributes(typeof(DisplayNameAttribute), true).ToList();
            if (attributes.Count > 0)
            {
                var displayNameAttribute = attributes[0] as DisplayNameAttribute;
                if (displayNameAttribute != null)
                {
                    _displayName = displayNameAttribute.DisplayName;
                    return base.IsValid(value, validationContext);
                }
            }
            _displayName = validationContext.MemberName;
            return base.IsValid(value, validationContext);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, _displayName);
        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metaData, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "required",
                ErrorMessage = FormatErrorMessage(metaData.DisplayName)
            };
            yield return rule;
        }

    }
}