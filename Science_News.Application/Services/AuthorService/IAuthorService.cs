using Science_News.Application.Models.DTOs.Author;
using Science_News.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Science_News.Application.Services.AuthorService
{
    public interface IAuthorService
    {
        Task Create(CreateAuthorDTO model);
        Task Update(UpdateAuthorDTO model);
        Task Delete(int id);

        Task<List<AuthorVM>> GetAuthors();

        Task<AuthorDetailsVM> GetDetails(int id);

        Task<UpdateAuthorDTO> GetById(int id);

        Task<bool> isAuthorExsist(string Firstname, string LastName);
    }
}
