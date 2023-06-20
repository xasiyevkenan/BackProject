using BackProject.DAL;
using BackProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly int _courseCount;
        private readonly int _blogCount;

        public HomeController(AppDbContext dbcontext)
        {
            _dbContext = dbcontext;
            _courseCount = _dbContext.Courses.Count();
            _blogCount = _dbContext.Blogs.Count();
        }

        public IActionResult Index()
        {
            ViewBag.CourseCount = _courseCount;
            ViewBag.BlogCount = _blogCount;

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

        public IActionResult LoadCourses(int skip)
        {
            var courses = _dbContext.Courses?.Skip(skip).Take(3).ToList();

            return PartialView("_CoursePartial", courses);
        }

        public IActionResult LoadBlogs(int skip)
        {
            var blogs = _dbContext.Blogs?.Skip(skip).Take(3).ToList();

            return PartialView("_BlogPartial", blogs);
        }
    }
}
