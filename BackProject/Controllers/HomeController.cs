using BackProject.DAL;
using BackProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public IActionResult Index()
        {
            var mainslider = _dbContext.MainSlider.ToList();
            var services = _dbContext.ServiceArea.ToList();
            var about = _dbContext.AboutArea.FirstOrDefault();
            var courses = _dbContext.Courses.Take(3).ToList();
            var notice = _dbContext.NoticeAreas.Include(x => x.NoticeBoards).FirstOrDefault();
            var events = _dbContext.Events.Take(4).ToList();
            var test = _dbContext.TestImonialArea.FirstOrDefault();
            var blogs = _dbContext.Blogs.Take(3).ToList();
            var logos = _dbContext.Logos.FirstOrDefault();
            var subscribe = _dbContext.Subscribe.FirstOrDefault();

            var viewModel = new HomeViewModel()
            {
                MainSlider = mainslider,
                ServiceArea = services,
                AboutArea = about,
                Courses = courses,
                NoticeArea = notice,
                Events = events,
                TestImonialArea = test,
                Blogs = blogs,
                Logos = logos,
                Subscribe = subscribe,
            };

            return View(viewModel);
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
