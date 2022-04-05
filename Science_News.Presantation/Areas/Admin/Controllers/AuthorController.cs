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
                var result = await _authorService.isAuthorExsist(model.FirstName, model.LastName);
                if (!result)
                {
                    await _authorService.Create(model);
                    TempData["Success"] = $"{model.FirstName}{model.LastName} has been added..!";
                    return RedirectToAction("List");

                }
                else
                {
                    TempData["Error"] = $"Author is already exsist..!";
                    return View(model);
                }
               
            }
            else
            {
                TempData["Error"] = $"Author hasn't been added..!";
                return View(model);
            }

        }

        public async Task<IActionResult> List()
        {
            var result = await _authorService.GetAuthors();

            return View(result);


        }

        public async  Task<IActionResult> Details(int id)
        {
            var authors = await _authorService.GetDetails(id);
            return View(authors);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            return View(await _authorService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateAuthorDTO model)
        {
            if (ModelState.IsValid)
            {
                await _authorService.Update(model);
                TempData["Success"] = "Author has been updated..!!";
                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "Author hasn't been updated..!!";
                
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
