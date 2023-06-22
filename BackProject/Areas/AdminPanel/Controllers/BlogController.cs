using BackProject.DAL.Entities;
using BackProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackProject.Areas.AdminPanel.Data;
using Files = System.IO.File;

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

            var isExist = await _dbContext.Blogs
                .AnyAsync(x => x.BlogTop.ToUpper() == Blog.BlogTop.ToUpper());

            if (isExist)
            {
                ModelState.AddModelError("BlogTop", "Bu Blog Artıq Mövcuddur");

                return View();
            }

            if (!Blog.Image.ContentType.Contains("image"))
            {
                ModelState.AddModelError("Image", "Şəkil seçməlisiniz!");

                return View();
            }

            if (Blog.Image.Length > 1024 * 1024)
            {
                ModelState.AddModelError("Image", "Şəkil 1mb-dan çox olmamalıdır");

                return View();
            }

            var pathBlog = Path.Combine(Constants.ImagePath, "Blog");

            var pathGuid = $"{Guid.NewGuid()}-{Blog.Image.FileName}";

            var path = Path.Combine(pathBlog, pathGuid);

            var pathUrl = Path.Combine("img", "Blog", pathGuid);

            Blog.ImageUrl = pathUrl;

            var fs = new FileStream(path, FileMode.CreateNew);

            await Blog.Image.CopyToAsync(fs);

            fs.Close();

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

            var newPath = Blog.ImageUrl.Remove(0, 4);

            var path = Path.Combine(Constants.ImagePath, newPath);

            if (Files.Exists(path))
            {
                Files.Delete(path);
            }

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

            var trimmedName = existBlog.ImageUrl.Remove(0, 4);

            var pathForDelete = Path.Combine(Constants.ImagePath, trimmedName);

            if (Files.Exists(pathForDelete))
            {
                Files.Delete(pathForDelete);
            }

            var path = Path.Combine(Constants.ImagePath, "Blog");

            var fileName = await Blog.Image.GenerateFile(path);

            existBlog.ImageUrl = $"img/Blog/{fileName}";

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
