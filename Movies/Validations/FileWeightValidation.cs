using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Validations
{
    public class FileWeightValidation: ValidationAttribute
    {
        private readonly int maxSizeMegaBytes;
        public FileWeightValidation(int MaxSizeMegaBytes)
        {
            maxSizeMegaBytes = MaxSizeMegaBytes;
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

            if (file.Length > maxSizeMegaBytes * 1024 * 1024)
            {
                return new ValidationResult($"El peso de la imagen no debe ser mayor a {maxSizeMegaBytes}mb.");
            }

            return ValidationResult.Success;
        }
    }
}
