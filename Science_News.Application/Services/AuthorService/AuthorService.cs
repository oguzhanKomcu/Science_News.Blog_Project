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

            if (author.ImagePath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(600, 560));
                Guid guid = Guid.NewGuid();
                image.Save($"/wwwroot/images/{guid}.jpg");
                author.ImagePath = ($"/wwwroot/images/{guid}.jpg");
                await _authorRepo.Create(author);
            }
            else
            {
                author.ImagePath = ($"/wwwroot/images/default.jpg");
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

        public Task<List<AuthorVM>> GetAuthors()
        {
           var author = _authorRepo.GetFilteredList(
               select : x => new AuthorVM
               {
                   Id = x.Id,
                   FirstName = x.FirstName,
                   LastName = x.LastName,
                   ImagePath = x.ImagePath,
                   

               },
               where: x=> x.Status != Status.Passive,
               orderBy: x => x.OrderBy(y => y.FirstName));

            return author;
        }

        public async Task<UpdateAuthorDTO> GetById(int id)
        {

            var author = await _authorRepo.GetFilteredFirstOrDefault(
                select: x => new AuthorVM
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ImagePath = x.ImagePath,
                },
                where: x => x.Id == id);

            var model = _mapper.Map<UpdateAuthorDTO>(author);

            return model;
        }

        public Task<List<AuthorDetailsVM>>  GetDetails(int id)
        {
            var author = _authorRepo.GetFilteredList(
              select: x => new AuthorDetailsVM
              {
                  Id = x.Id,
                  FirstName = x.FirstName,
                  LastName = x.LastName,
                  ImagePath = x.ImagePath,
                  CreateDate = x.CreateDate,    



              },
              where: x => x.Status != Status.Passive,
              orderBy: x => x.OrderBy(y => y.FirstName));

            return author;
        }

        public Task<bool> isAuthorExsist(string name)
        {
            var result = _authorRepo.Any(x => x.FirstName == name);
            return result;
        }

        public async Task Update(UpdateAuthorDTO model)
        {
            var updateAuthor = _mapper.Map<Author>(model);
            await _authorRepo.Update(updateAuthor);

        }
    }
}
