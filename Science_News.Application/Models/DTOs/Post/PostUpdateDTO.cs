using Microsoft.AspNetCore.Http;
using Science_News.Application.Extensions;
using Science_News.Application.Models.VMs;
using Science_News.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Science_News.Application.Models.DTOs.Post
{
    public class PostUpdateDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }

        [PictureFileExtension]
        public IFormFile? UploadPath { get; set; }


        public string? ImagePath { get; set; }

        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;

        public int CategoryId { get; set; }
        public int AuthorId { get; set; }

        public List<AuthorVM> Authors { get; set; }
        public List<CategoryVM> Categories { get; set; }


    }
}
