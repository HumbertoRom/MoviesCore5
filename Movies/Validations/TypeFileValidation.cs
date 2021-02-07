using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Validations
{
    public class TypeFileValidation : ValidationAttribute
    {
        private readonly string[] validTypes;
        public TypeFileValidation(string[] validTypes)
        {
            this.validTypes = validTypes;
        }

        public TypeFileValidation(TypeFile typeFile)
        {
            if (typeFile == TypeFile.Image)
            {
                validTypes = new string[] { "image/jpeg", "image/png", "image/gif" };
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile file = value as IFormFile;
            if (file == null)
            {
                return ValidationResult.Success;
            }

            if (!validTypes.Contains(file.ContentType))
            {
                return new ValidationResult($"El tipo del archivo debe ser uno de los siguientes: {string.Join(", ", validTypes)}.");
            }

            return ValidationResult.Success;
        }
    }

    public enum TypeFile
    {
        Image
    }
}
