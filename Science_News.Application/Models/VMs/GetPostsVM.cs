using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Science_News.Application.Models.VMs
{
    public class GetPostsVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public string CategoryName { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string ImagePath { get; set; }

        public DateTime CreateDate { get; set; }
        public string FullName => $"{AuthorFirstName} {AuthorLastName}";
        public string UserImagePath { get; set; }
    }
}
