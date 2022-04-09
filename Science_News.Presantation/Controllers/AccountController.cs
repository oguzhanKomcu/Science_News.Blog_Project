using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Science_News.Application.Models.DTOs.AppUser;
using Science_News.Application.Services.AppUserService;

namespace Science_News.Presantation.Controllers
{


    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly IAppUserService _appUserService;

        public AccountController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        

        [AllowAnonymous]
        
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
                
            }
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if(ModelState.IsValid)   
            {
                var result = await _appUserService.Register(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    TempData["Error"] = "Something went wrong..!!";
                }

            }
            return View(model);
        }


        public IActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
            }

            ViewData["ReturnUrl"] = returnUrl ;
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _appUserService.Login(model);
                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                ModelState.AddModelError("", "Invalid login Attapt.");


            }
            return View(model);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {   
            
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
            }
        }
    }
}
