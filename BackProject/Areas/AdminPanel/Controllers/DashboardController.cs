using Microsoft.AspNetCore.Mvc;

namespace BackProject.Areas.AdminPanel.Controllers
{
    public class DashboardController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
