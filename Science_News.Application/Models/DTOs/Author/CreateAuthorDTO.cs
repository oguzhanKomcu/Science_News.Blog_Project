using Microsoft.AspNetCore.Http;
using Science_News.Application.Extensions;
using Science_News.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Science_News.Application.Models.DTOs.Author
{
    public class CreateAuthorDTO
    {

        [Required(ErrorMessage = "Must to type first name")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Must to type first name")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string LastName { get; set; }

        [PictureFileExtension] 
        public IFormFile? UploadPath { get; set; }


        public string? ImagePath { get; set; }

        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
    }
}
