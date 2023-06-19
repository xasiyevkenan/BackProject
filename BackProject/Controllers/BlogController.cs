using BackProject.DAL;
using BackProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BackProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _dbContext;

        public BlogController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var blogs = _dbContext.Blogs.ToList();
            var subscribe = _dbContext.Subscribe.FirstOrDefault();

            BlogViewModel viewModel = new BlogViewModel
            {
                Blogs = blogs,
                Subscribe = subscribe
            };

            return View(viewModel);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var blogs = _dbContext.Blogs.Find(id);

            if (blogs == null)
            {
                return NotFound();
            }

            return View(blogs);
        }
    }
}
