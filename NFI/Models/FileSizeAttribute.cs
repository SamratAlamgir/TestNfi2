using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace NFI.Models
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FileSizeAttribute : ValidationAttribute, IClientValidatable
    {
        public int? MaxBytes { get; set; }

        public FileSizeAttribute() : base("Please Upload a file")
        {
            MaxBytes = 10 * 1024 * 1024;
            ErrorMessage = "Please upload a file of less than " + MaxBytes.Value / (1024 * 1024) + " MB.";
        }

        public FileSizeAttribute(int maxBytes)
            : base("Please upload a supported file.")
        {
            MaxBytes = maxBytes;
            if (MaxBytes.HasValue)
            {
                ErrorMessage = "Please upload a file of less than " + MaxBytes.Value / (1024 * 1024) + " MB.";
            }
        }

        public override bool IsValid(object value)
        {
            HttpPostedFileBase file = value as HttpPostedFileBase;
            if (file != null)
            {
                bool result = true;

                if (MaxBytes.HasValue)
                {
                    result &= (file.ContentLength < MaxBytes.Value);
                }

                return result;
            }

            return true;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metaData, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "filesize",
                ErrorMessage = FormatErrorMessage(metaData.DisplayName)
            };
            rule.ValidationParameters["maxbytes"] = MaxBytes;
            yield return rule;
        }
    }

}