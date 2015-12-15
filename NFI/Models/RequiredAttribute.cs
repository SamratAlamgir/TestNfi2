using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NFI.Models
{
    public class RequiredAttribute : System.ComponentModel.DataAnnotations.RequiredAttribute
    {
        private string _displayName;

        public RequiredAttribute()
        {
            ErrorMessage = "{0} er obligatorisk";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var attributes =
                validationContext.ObjectType.GetProperty(validationContext.MemberName)
                    .GetCustomAttributes(typeof (DisplayNameAttribute), true);
            var displayNameAttribute = attributes[0] as DisplayNameAttribute;
            if (displayNameAttribute != null)
                _displayName = displayNameAttribute.DisplayName;

            return base.IsValid(value, validationContext);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, _displayName);
        }
       
    }
}