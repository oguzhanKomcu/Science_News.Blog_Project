using Science_News.Domain.Entities;
using Science_News.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Science_News.Infrastructure.Repositories
{
    public class CategoryRepo : BaseRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
