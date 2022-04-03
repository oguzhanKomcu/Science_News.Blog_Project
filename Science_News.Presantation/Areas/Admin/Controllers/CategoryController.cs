using Microsoft.AspNetCore.Mvc;
using Science_News.Application.Models.DTOs.Category;
using Science_News.Application.Services.CategoryService;

namespace Science_News.Presantation.Areas.Admin.Controllers
{
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
            
            
            if   (ModelState.IsValid)
            {
                await _categoryService.Create(model);
                return RedirectToAction("List");
            }   
            else
            {
                return View(model);
            }

        }

        public async Task<IActionResult> List()
        {
            var categories = await _categoryService.GetGenres();
            return View(categories);
        }
    }
}
