using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using Science_News.Application.Models.DTOs.AppUser;
using Science_News.Domain.Entities;
using Science_News.Domain.Repositories;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Science_News.Application.Services.AppUserService
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepo _appUserRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AppUserService(IAppUserRepo appUserRepo, IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _appUserRepo = appUserRepo;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<UpdateUserProfilDTO> GetByUserName(string userName)
        {
            var user = await _appUserRepo.GetFilteredFirstOrDefault(
                select: x => new UpdateUserProfilDTO
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Password = x.PasswordHash,
                    Email = x.Email,
                    ImagePath = x.ImagePath,
                },
                where: x => x.UserName == userName);


            return user;

        }

       

        public async Task<SignInResult> Login(LoginDTO model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            return result;
        }



        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterDTO model)
        {
            var user = _mapper.Map<AppUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;
        }

        public async Task UpdateUser(UpdateUserProfilDTO model)
        {
            var user = _mapper.Map<AppUser>(model);
            if (user.UploadPath != null)
            {

                using var image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(600, 560));
                string guid = Guid.NewGuid().ToString();
                image.Save($"wwwroot/images/appuser/{guid}.jpg");
                user.ImagePath = $"/images/appuser/{guid}.jpg";
                await _appUserRepo.Create(user);

            }

            if (model.Password != null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
                await _userManager.UpdateAsync(user);
            }

            if (model.UserName != null)
            {
                var isUserNameExist = await _userManager.FindByNameAsync(model.UserName);
                if (isUserNameExist == null)
                {
                    await _userManager.SetUserNameAsync(user, model.UserName);
                }
            }

            if (model.Email != null)
            {
                var isUserEmailExist = await _userManager.FindByEmailAsync(model.Email);
                if (isUserEmailExist == null)
                {
                    await _userManager.SetEmailAsync(user, model.Email);
                }
            }
        }
    }
}
