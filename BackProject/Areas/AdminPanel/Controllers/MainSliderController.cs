using BackProject.DAL.Entities;
using BackProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackProject.Areas.AdminPanel.Data;
using Files = System.IO.File;


namespace BackProject.Areas.AdminPanel.Controllers
{
    public class MainSliderController : AdminController
    {
        private readonly AppDbContext _dbContext;

        public MainSliderController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var MainSlider = await _dbContext.MainSlider.ToListAsync();

            return View(MainSlider);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var MainSlider = await _dbContext.MainSlider.FindAsync(id);

            if (MainSlider == null)
            {
                return NotFound();
            }

            return View(MainSlider);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MainSlider MainSlider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExist = await _dbContext.MainSlider
                    .AnyAsync(x => x.Title.ToUpper() == MainSlider.Title.ToUpper());

            if (isExist)
            {
                ModelState.AddModelError("Title", "Bu Müəllim Artıq Mövcuddur");

                return View();
            }

            if (!MainSlider.Image.ContentType.Contains("image"))
            {
                ModelState.AddModelError("Image", "Şəkil seçməlisiniz!");

                return View();
            }

            if (MainSlider.Image.Length > 1024 * 1024)
            {
                ModelState.AddModelError("Image", "Şəkil 1mb-dan çox olmamalıdır");

                return View();
            }

            var pathMainSlider = Path.Combine(Constants.ImagePath, "MainSlider");

            var pathGuid = $"{Guid.NewGuid()}-{MainSlider.Image.FileName}";

            var path = Path.Combine(pathMainSlider, pathGuid);

            var pathUrl = Path.Combine("img", "MainSlider", pathGuid);

            MainSlider.ImageUrl = pathUrl;

            var fs = new FileStream(path, FileMode.CreateNew);

            await MainSlider.Image.CopyToAsync(fs);

            fs.Close();

            await _dbContext.MainSlider.AddAsync(MainSlider);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var MainSlider = await _dbContext.MainSlider.FindAsync(id);

            if (MainSlider == null) return NotFound();

            var newPath = MainSlider.ImageUrl.Remove(0, 4);

            var path = Path.Combine(Constants.ImagePath, newPath);

            if (Files.Exists(path))
            {
                Files.Delete(path);
            }

            _dbContext.MainSlider.Remove(MainSlider);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            var MainSlider = await _dbContext.MainSlider.FindAsync(id);

            if (MainSlider == null) return NotFound();

            return View(MainSlider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, MainSlider MainSlider)
        {
            if (id == null) return NotFound();

            if (id != MainSlider.Id) return BadRequest();

            var existMainSlider = await _dbContext.MainSlider.FindAsync(id);

            existMainSlider.Title = MainSlider.Title;

            existMainSlider.Description = MainSlider.Description;

            var isExist = await _dbContext.MainSlider
                    .AnyAsync(x => x.Title.ToUpper() == MainSlider.Title.ToUpper() && x.Id != id);

            if (isExist)
            {
                ModelState.AddModelError("Title", "Bu Müəllim Artıq Mövcuddur");
                return View();
            }

            var trimmedTitle = existMainSlider.ImageUrl.Remove(0, 4);

            var pathForDelete = Path.Combine(Constants.ImagePath, trimmedTitle);

            if (Files.Exists(pathForDelete))
            {
                Files.Delete(pathForDelete);
            }

            var path = Path.Combine(Constants.ImagePath, "MainSlider");

            var FileName = await MainSlider.Image.GenerateFile(path);

            existMainSlider.ImageUrl = $"img/MainSlider/{FileName}";

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
