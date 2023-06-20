using Microsoft.AspNetCore.Mvc;

namespace BackProject.Controllers
{
    public class SignupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
