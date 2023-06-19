using BackProject.DAL;
using BackProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackProject.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _dbContext;

        public AboutController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var about = _dbContext.AboutArea.FirstOrDefault();
            var teachers = _dbContext.Teachers.Take(4).ToList();
            var test = _dbContext.TestImonialArea.FirstOrDefault();
            var notice = _dbContext.NoticeAreas.Include(x => x.NoticeBoards).FirstOrDefault();
            var subscribe = _dbContext.Subscribe.FirstOrDefault();
            var logos = _dbContext.Logos.FirstOrDefault();

            AboutViewModel viewModel = new AboutViewModel
            {
                About = about,
                Teachers = teachers,
                Test = test,
                Notice = notice,
                Subscribe = subscribe,
                Logos = logos,
            };

            return View(viewModel);
        }
    }
}
