using AutoMapper;
using Science_News.Application.Models.DTOs.Category;
using Science_News.Application.Models.VMs;
using Science_News.Domain.Entities;
using Science_News.Domain.Enums;
using Science_News.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Science_News.Application.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;


        public CategoryService(ICategoryRepo categoryRepo , IMapper mapper )
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;       
        }
        


        public async Task Create(CreateCategoryDTO model)
        {
            var category = _mapper.Map<Category>(model);
            
            await _categoryRepo.Create(category);
        }

        public async Task Delete(int id)
        {
            Category categery = await _categoryRepo.GetDefault(x => x.Id == id);
            categery.DeleteDate = DateTime.Now;
            categery.Status = Status.Passive;  
            await _categoryRepo.Delete(categery);
        }

        public async Task<UpdateCategoryDTO> GetById(int id)
        {
            var category = await _categoryRepo.GetFilteredFirstOrDefault(
                select: x => new CategoryVM
                {
                    Id = x.Id,
                    Name = x.Name,
                   
                },
                where: x => x.Id == id);

            var model = _mapper.Map<UpdateCategoryDTO>(category);
            return model;

        }

        public Task<List<CategoryVM>> GetGenres()
        {
            var category = _categoryRepo.GetFilteredList(
                 select: x => new CategoryVM
                 {
                     Id = x.Id,
                     Name = x.Name,

                 },
                 where: x => x.Status != Status.Passive,
                 orderBy: x => x.OrderBy(y => y.Name));

            return category;
        }

        public Task<bool> isCategoryExsist(string Name)
        {
            var result = _categoryRepo.Any(x => x.Name == Name);
            return result;
        }

        public Task Update(UpdateCategoryDTO model)
        {
            var updateCategory = _mapper.Map<Category>(model);
            return _categoryRepo.Update(updateCategory);    
        }
    }
}
