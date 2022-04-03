using Science_News.Domain.Entities;
using Science_News.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Science_News.Infrastructure.Repositories
{
    public class AppUserRepo : BaseRepo<AppUser>, IAppUserRepo
    {
        public AppUserRepo(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
