using BackProject.DAL.Entities;
using BackProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            var isExist = await _dbContext.MainSlider.AnyAsync(x => x.Title.ToUpper() == MainSlider.Title.ToUpper());

            if (isExist)
            {
                ModelState.AddModelError("Title", "Bu Kurs Artıq Mövcuddur");
                return View();
            }

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

            var isExist = await _dbContext.MainSlider.AnyAsync(x => x.Title.ToUpper() == MainSlider.Title.ToUpper() && x.Id != id);

            if (isExist)
            {
                ModelState.AddModelError("Title", "Bu Slider Artıq Mövcuddur");
                return View();
            }

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
