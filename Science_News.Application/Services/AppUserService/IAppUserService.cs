using Microsoft.AspNetCore.Identity;
using Science_News.Application.Models.DTOs.AppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Science_News.Application.Services.AppUserService
{
    public interface IAppUserService
    {
        Task<IdentityResult> Register(RegisterDTO model);

        Task<SignInResult> Login(LoginDTO model);

        Task LogOut();

        Task UpdateUser(UpdateUserProfilDTO model);

        Task<UpdateUserProfilDTO> GetByUserName(string userName);

    

        
    }
}
