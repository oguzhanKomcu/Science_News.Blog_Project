using Microsoft.AspNetCore.Mvc;
using Science_News.Application.Models.DTOs.Author;
using Science_News.Application.Services.AuthorService;

namespace Science_News.Presantation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }



        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorDTO model)
        {


            if (ModelState.IsValid)
            {
                await _authorService.Create(model);
                return RedirectToAction("List");
            }
            else
            {
                return View(model);
            }

        }

        public async Task<IActionResult> List()
        {
            var categories = await _authorService.GetAuthors();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> GetUpdate(int id)
        {

            return View(await _authorService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateAuthorDTO model)
        {
            if (ModelState.IsValid)
            {
                await _authorService.Update(model);
                return RedirectToAction("List");
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.Delete(id);
            return RedirectToAction("List");
        }
    }
}
