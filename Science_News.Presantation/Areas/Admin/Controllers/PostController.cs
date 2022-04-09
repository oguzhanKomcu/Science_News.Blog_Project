using Microsoft.AspNetCore.Mvc;
using Science_News.Application.Models.DTOs.Post;
using Science_News.Application.Services.PostService;

namespace Science_News.Presantation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
           this._postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(await _postService.CreatePost());
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _postService.Create(model);
                TempData["Success"] = "Post has been added..!!";
                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "Post has not been added..!!";
                return View(model);
            }

        }


        public async Task<IActionResult> List()
        {
            return View(await _postService.GetPosts());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _postService.PostDetails(id));
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            return View(await _postService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(PostUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _postService.Update(model);
                TempData["Success"] = "Post has been added..!!";
                return RedirectToAction("List");


            }
            else
            {
                TempData["Error"] = "Post hasn't been added..!!";
                return View(model);
            }

        }

        
    }
}
