using AutoMapper;
using Science_News.Application.Models.DTOs.Author;
using Science_News.Application.Models.DTOs.Category;
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

namespace Science_News.Application.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepo _authorRepo;
        private readonly IMapper _mapper;

        public AuthorService(IMapper mapper,
                             IAuthorRepo authorRepo)
        {
            _authorRepo = authorRepo;
            _mapper = mapper;
        }

        public async Task Create(CreateAuthorDTO model)
        {
            var author = _mapper.Map<Author>(model);

            if (author.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(600, 560));
                string guid = Guid.NewGuid().ToString();
                image.Save($"wwwroot/images/author/{guid}.jpg");
                author.ImagePath = $"/images/author/{guid}.jpg";
                await _authorRepo.Create(author);
            }
            else
            {
                author.ImagePath = ($"/images/default.jpg");
                await _authorRepo.Create(author);
            }
        }

        public async Task Delete(int id)
        {
            var author = await _authorRepo.GetDefault(x => x.Id == id);
            author.DeleteDate = DateTime.Now;
            author.Status = Status.Passive;
            await _authorRepo.Delete(author);
        }

        public async Task<List<AuthorVM>> GetAuthors()
        {
            var authors = await _authorRepo.GetFilteredList(
                select: x => new AuthorVM
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ImagePath = x.ImagePath,
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.FirstName));

            return authors;

        }

        public async Task<UpdateAuthorDTO> GetById(int id)
        {

            var author = await _authorRepo.GetFilteredFirstOrDefault(
                select: x => new UpdateAuthorDTO
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ImagePath = x.ImagePath,
                },
                where: x => x.Id == id,
                 orderBy: x => x.OrderBy(x => x.FirstName));

            //var model = _mapper.Map<UpdateAuthorDTO>(author);

            return author;
        }

        public async Task<AuthorDetailsVM> GetDetails(int id)
        {
            var authors = await _authorRepo.GetFilteredFirstOrDefault(
                select: x => new AuthorDetailsVM
                {

                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ImagePath = x.ImagePath,
                    CreateDate = x.CreateDate
                },
                where: x => x.Id == id);

            return authors;
        }

        public Task<bool> isAuthorExsist(string Firstname, string LastName)
        {
            var result = _authorRepo.Any(x => x.FirstName == Firstname && x.LastName == LastName);
            return result;
        }

        public async Task Update(UpdateAuthorDTO model)
        {
            var author = _mapper.Map<Author>(model);

            if (author.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(600, 560));
                string guid = Guid.NewGuid().ToString();
                image.Save($"wwwroot/images/author/{guid}.jpg");
                author.ImagePath = $"/images/author/{guid}.jpg";
                await _authorRepo.Create(author);
            }
            else
            {
                author.ImagePath = model.ImagePath;
                await _authorRepo.Update(author);
            }

        }
    }
}
