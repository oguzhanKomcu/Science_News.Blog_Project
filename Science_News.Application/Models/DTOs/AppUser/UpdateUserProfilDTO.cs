using Microsoft.AspNetCore.Http;
using Science_News.Application.Extensions;
using Science_News.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Science_News.Application.Models.DTOs.AppUser
{
    public class UpdateUserProfilDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please enter your user name")]
        [Display (Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Please enter your confirm password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Adress")]
        public string Email { get; set; }

        public string? ImagePath { get; set; }

        [PictureFileExtension]
        public IFormFile? UploadPath { get; set; }

        public DateTime UpdateDate => DateTime.Now;

        public Status Status => Status.Modified;


    }
}
