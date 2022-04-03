using Microsoft.AspNetCore.Mvc;

namespace Science_News.Presantation.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
