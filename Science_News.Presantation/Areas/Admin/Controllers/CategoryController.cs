using Microsoft.AspNetCore.Mvc;
using Science_News.Application.Models.DTOs.Category;
using Science_News.Application.Services.CategoryService;

namespace Science_News.Presantation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        
           
        
        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> Create(CreateCategoryDTO model)
        {


            if (ModelState.IsValid)
            {

                bool result = await _categoryService.isCategoryExsist(model.Name);
                if (!result)
                {
                    TempData["Success"] = $"{model.Name} has been added..!!";
                    await _categoryService.Create(model);
                    return RedirectToAction("List");
                }
                else
                {
                    TempData["Warning"] = $"{model.Name} already has been added..!!";
                    return View(model);
                }

            }
            else
            {
                TempData["Erorr"] = $"{model.Name} hasn't been added..!!";
                return View(model);
            }

        }

        public async Task<IActionResult> List()
        {
            var categories = await _categoryService.GetGenres();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
           
            return View(await _categoryService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Update(model);
                return RedirectToAction("List");
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.Delete(id);
            return RedirectToAction("List");
        }
    }
}
