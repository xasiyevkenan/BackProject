using BackProject.DAL.Entities;
using BackProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackProject.Areas.AdminPanel.Controllers
{
    public class BlogController : AdminController
    {
        private readonly AppDbContext _dbContext;

        public BlogController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var Blogs = await _dbContext.Blogs.ToListAsync();

            return View(Blogs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var Blog = await _dbContext.Blogs.FindAsync(id);

            if (Blog == null)
            {
                return NotFound();
            }

            return View(Blog);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blogs Blog)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExist = await _dbContext.Blogs.AnyAsync(x => x.BlogTop.ToUpper() == Blog.BlogTop.ToUpper());

            if (isExist)
            {
                ModelState.AddModelError("BlogTop", "Bu Blog Artıq Mövcuddur");
                return View();
            }

            await _dbContext.Blogs.AddAsync(Blog);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var Blog = await _dbContext.Blogs.FindAsync(id);

            if (Blog == null) return NotFound();

            _dbContext.Blogs.Remove(Blog);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            var Blog = await _dbContext.Blogs.FindAsync(id);

            if (Blog == null) return NotFound();

            return View(Blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Blogs Blog)
        {
            if (id == null) return NotFound();

            if (id != Blog.Id) return BadRequest();

            var existBlog = await _dbContext.Blogs.FindAsync(id);

            existBlog.BlogTop = Blog.BlogTop;

            existBlog.Description = Blog.Description;

            var isExist = await _dbContext.Blogs.AnyAsync(x => x.BlogTop.ToUpper() == Blog.BlogTop.ToUpper() && x.Id != id);

            if (isExist)
            {
                ModelState.AddModelError("BlogTop", "Bu Kurs Artıq Mövcuddur");
                return View();
            }

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
