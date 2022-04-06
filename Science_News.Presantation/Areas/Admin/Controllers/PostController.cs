using Microsoft.AspNetCore.Mvc;

namespace Science_News.Presantation.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
