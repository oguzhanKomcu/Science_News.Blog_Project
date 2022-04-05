using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Science_News.Application.Extensions
{
    public class PictureFileExtensionAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            IFormFile file = value as IFormFile;

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                string[] extensions = { "jpg", "png", "jpeg" };  // add more extensions here

                bool result = extensions.Any(x => extension.ToLower().EndsWith(x));

                if (!result)
                {
                    return new ValidationResult("Valid format is 'jpg', 'jpeg','png'");
                }
            }

            return ValidationResult.Success;  
        }
    }
 
}
