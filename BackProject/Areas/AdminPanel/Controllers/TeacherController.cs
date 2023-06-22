using BackProject.DAL.Entities;
using BackProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackProject.Areas.AdminPanel.Data;
using Files = System.IO.File;

namespace BackProject.Areas.AdminPanel.Controllers
{
    public class teacherController : AdminController
    {

        private readonly AppDbContext _dbContext;

        public teacherController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var Teachers = await _dbContext.Teachers.ToListAsync();

            return View(Teachers);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var teacher = await _dbContext.Teachers.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teachers teacher)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExist = await _dbContext.Teachers
                .AnyAsync(x => x.Name.ToUpper() == teacher.Name.ToUpper());

            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu Müəllim Artıq Mövcuddur");

                return View();
            }

            if (!teacher.Image.ContentType.Contains("image"))
            {
                ModelState.AddModelError("Image", "Şəkil seçməlisiniz!");

                return View();
            }

            if (teacher.Image.Length > 1024 * 1024)
            {
                ModelState.AddModelError("Image", "Şəkil 1mb-dan çox olmamalıdır");

                return View();
            }

            var pathteacher = Path.Combine(Constants.ImagePath, "teacher");

            var pathGuid = $"{Guid.NewGuid()}-{teacher.Image.FileName}";

            var path = Path.Combine(pathteacher, pathGuid);

            var pathUrl = Path.Combine("img", "teacher", pathGuid);

            teacher.ImageUrl = pathUrl;

            var fs = new FileStream(path, FileMode.CreateNew);

            await teacher.Image.CopyToAsync(fs);

            fs.Close();

            await _dbContext.Teachers.AddAsync(teacher);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var teacher = await _dbContext.Teachers.FindAsync(id);

            if (teacher == null) return NotFound();

            var newPath = teacher.ImageUrl.Remove(0, 4);

            var path = Path.Combine(Constants.ImagePath, newPath);

            if (Files.Exists(path))
            {
                Files.Delete(path);
            }

            _dbContext.Teachers.Remove(teacher);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            var teacher = await _dbContext.Teachers.FindAsync(id);

            if (teacher == null) return NotFound();

            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Teachers teacher)
        {
            if (id == null) return NotFound();

            if (id != teacher.Id) return BadRequest();

            var existTeacher = await _dbContext.Teachers.FindAsync(id);

            existTeacher.Name = teacher.Name;

            existTeacher.Degree = teacher.Degree;

            var isExist = await _dbContext.Teachers
                 .AnyAsync(x => x.Name.ToUpper() == teacher.Name.ToUpper() && x.Id != id);

            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu Müəllim Artıq Mövcuddur");
                return View();
            }

            var trimmedName = existTeacher.ImageUrl.Remove(0, 4);

            var pathForDelete = Path.Combine(Constants.ImagePath, trimmedName);

            if (Files.Exists(pathForDelete))
            {
                Files.Delete(pathForDelete);
            }

            var path = Path.Combine(Constants.ImagePath, "Teacher");

            var fileName = await teacher.Image.GenerateFile(path);

            existTeacher.ImageUrl = $"img/Teacher/{fileName}";

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
