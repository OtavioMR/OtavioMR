using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers
{
    public class SkillController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
