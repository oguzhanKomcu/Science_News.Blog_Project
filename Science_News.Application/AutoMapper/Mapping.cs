using AutoMapper;
using Science_News.Application.Models.DTOs.Author;
using Science_News.Application.Models.DTOs.Category;
using Science_News.Application.Models.DTOs.Post;
using Science_News.Application.Models.VMs;
using Science_News.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Science_News.Application.AutoMapper
{
    public class Mapping : Profile
    {

        public Mapping()
        {


                CreateMap<Category, CreateCategoryDTO>().ReverseMap();
                CreateMap<Category, UpdateCategoryDTO>().ReverseMap();
                CreateMap<Category, CategoryVM>().ReverseMap();
                CreateMap<UpdateCategoryDTO, CategoryVM>().ReverseMap();



                CreateMap<Author, CreateAuthorDTO>().ReverseMap();
                CreateMap<Author, UpdateAuthorDTO>().ReverseMap();
                CreateMap<Author, AuthorVM>().ReverseMap();
                CreateMap<UpdateAuthorDTO, AuthorVM>().ReverseMap();


            CreateMap<Post, PostCreateDTO>().ReverseMap();
            CreateMap<Post, PostUpdateDTO>().ReverseMap();
            CreateMap<Post, PostVM>().ReverseMap();
            CreateMap<PostUpdateDTO, PostVM>().ReverseMap();


        }

    }
}
