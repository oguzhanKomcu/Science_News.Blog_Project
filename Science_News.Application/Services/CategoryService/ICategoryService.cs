using Science_News.Application.Models.DTOs.Category;
using Science_News.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Science_News.Application.Services.CategoryService
{
    public interface ICategoryService
    {
        Task Create(CreateCategoryDTO model);
        Task Update(UpdateCategoryDTO model);
        Task Delete(int id);

        Task<List<CategoryVM>> GetGenres();

        Task<UpdateCategoryDTO> GetById(int id);

        Task<bool> isGenreExsist(string Name);
    }
}
