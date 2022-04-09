using Microsoft.AspNetCore.Mvc;
using Science_News.Application.Services.PostService;

namespace Science_News.Presantation.Areas.Member.Controllers
{
    [Area("Member")]
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        public HomeController(IPostService postService)
        {
            _postService = postService;

        }


        
        public async Task<IActionResult> Index()
        {
            return View(await _postService.GetPostsForMembers());
        }
    }
}
