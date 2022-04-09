using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Science_News.Application.Models.DTOs.Post;
using Science_News.Application.Models.VMs;
using Science_News.Domain.Entities;
using Science_News.Domain.Enums;
using Science_News.Domain.Repositories;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Science_News.Application.Services.PostService
{
    public class PostService : IPostService
    {

        private readonly IPostRepo _postRepo;
        private readonly IAuthorRepo _authorRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;

        public PostService(IPostRepo postRepo, IAuthorRepo authorRepo, ICategoryRepo categoryRepo,IMapper mapper)
        {
            _postRepo = postRepo;
            _authorRepo = authorRepo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public async Task Create(PostCreateDTO model)
        {
            var post = _mapper.Map<Post>(model);

            if (post.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(600, 560));
                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");
                post.ImagePath = ($"/image/{guid}.jpg");
                await _postRepo.Create(post);

            }
            else
            {
                post.ImagePath = $"/image/default.jpg";
                await _postRepo.Create(post);
            }


        }

        public async Task<PostCreateDTO> CreatePost()
        {
            PostCreateDTO model = new PostCreateDTO();
            model.Categories = await _categoryRepo.GetFilteredList(
                select: x => new CategoryVM()
                {
                    Id = x.Id,
                    Name = x.Name
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x=>x.Name)
                );

            model.Authors = await _authorRepo.GetFilteredList(
                select: x => new AuthorVM()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,

                },
                where: x => x.Status != Status.Passive,
                  orderBy: x => x.OrderBy(x => x.FirstName));

            return model;

        }

        public async Task Delete(int id)
        {
            var post = await _postRepo.GetDefault(x => x.Id == id);
            post.DeleteDate = DateTime.Now;
            post.Status = Status.Passive;
            await _postRepo.Delete(post);
        }

        public async Task<PostUpdateDTO> GetById(int id)
        {
            var post = await _postRepo.GetFilteredFirstOrDefault(
                select: x => new PostVM
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    ImagePath = x.ImagePath,
                    AuthorId = x.AuthorId,
                    CategoryId = x.CategoryId
                },
                where: x => x.Id == id);

            var model = _mapper.Map<PostUpdateDTO>(post);

            model.Authors = await _authorRepo.GetFilteredList(
                select: x => new AuthorVM
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.FirstName));

            model.Categories = await _categoryRepo.GetFilteredList(
                select: x => new CategoryVM
                {
                    Id = x.Id,
                    Name = x.Name
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Name));


            return model;



        }

        public Task<List<PostVM>> GetPosts()
        {
            var posts = _postRepo.GetFilteredList(
                select: x => new PostVM
                {
                    Id = x.Id,
                    Title = x.Title,
                    CategoryName = x.Category.Name,
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName,
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Title),
                include: x => x.Include(x => x.Category).Include(x => x.Author));

            return posts;




        }

        public async Task<List<GetPostsVM>> GetPostsForMembers()
        {
            var posts = await _postRepo.GetFilteredList(
                select: x => new GetPostsVM
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    ImagePath = x.ImagePath,
                    CategoryName = x.Category.Name,
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName,
                    CreateDate = x.CreateDate,
                    UserImagePath = x.Author.ImagePath,


                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderByDescending(x => x.CreateDate),
                include: x => x.Include(x => x.Author));

            return posts;
               
               
               
        }

        public async Task<PostDetailsVM> PostDetails(int id)
        {
            var post = await _postRepo.GetFilteredFirstOrDefault(
                select: x => new PostDetailsVM
                {
                    Title = x.Title,
                    Content = x.Content,
                    ImagePath = x.ImagePath,
                    CreateDate = x.CreateDate,
                    AuthorImageLast = x.Author.ImagePath,
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Title),
                include: x => x.Include(x => x.Author));

            return post;
        }

        public async Task Update(PostUpdateDTO model)
        {
            var post = _mapper.Map<Post>(model);
            if (post.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(600, 560));
                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");
                post.ImagePath = ($"/images/{guid}.jpg");

                await _postRepo.Update(post);
            }
            else
            {
                post.ImagePath = model.ImagePath;
                await _postRepo.Update(post);
            }
        }
    }
}
