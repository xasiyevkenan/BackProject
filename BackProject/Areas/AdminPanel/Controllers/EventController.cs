using BackProject.DAL.Entities;
using BackProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackProject.Areas.AdminPanel.Data;
using Files = System.IO.File;
using System.Reflection.Metadata;

namespace BackProject.Areas.AdminPanel.Controllers
{
    public class EventController : AdminController
    {
        private readonly AppDbContext _dbContext;

        public EventController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var Events = await _dbContext.Events.ToListAsync();

            return View(Events);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var Event = await _dbContext.Events.FindAsync(id);

            if (Event == null)
            {
                return NotFound();
            }

            return View(Event);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Events Event)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExist = await _dbContext.Events
                .AnyAsync(x => x.Title.ToUpper() == Event.Title.ToUpper());

            if (isExist)
            {
                ModelState.AddModelError("Title", "Bu Event Artıq Mövcuddur");

                return View();
            }

            if (!Event.Image.ContentType.Contains("image"))
            {
                ModelState.AddModelError("Image", "Şəkil seçməlisiniz!");

                return View();
            }

            if (Event.Image.Length > 1024 * 1024)
            {
                ModelState.AddModelError("Image", "Şəkil 1mb-dan çox olmamalıdır");

                return View();
            }

            var pathEvent = Path.Combine(Constants.ImagePath, "Event");

            var pathGuid = $"{Guid.NewGuid()}-{Event.Image.FileName}";

            var path = Path.Combine(pathEvent, pathGuid);

            var pathUrl = Path.Combine("img", "Event", pathGuid);

            Event.ImageUrl = pathUrl;

            var fs = new FileStream(path, FileMode.CreateNew);

            await Event.Image.CopyToAsync(fs);

            fs.Close();

            await _dbContext.Events.AddAsync(Event);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var Event = await _dbContext.Events.FindAsync(id);

            if (Event == null) return NotFound();

            var newPath = Event.ImageUrl.Remove(0, 4);

            var path = Path.Combine(Constants.ImagePath, newPath);

            if (Files.Exists(path))
            {
                Files.Delete(path);
            }

            _dbContext.Events.Remove(Event);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            var Event = await _dbContext.Events.FindAsync(id);

            if (Event == null) return NotFound();

            return View(Event);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Events Event)
        {
            if (id == null) return NotFound();

            if (id != Event.Id) return BadRequest();

            var existEvent = await _dbContext.Events.FindAsync(id);

            existEvent.Title = Event.Title;

            existEvent.Time = Event.Time;

            existEvent.Date = Event.Date;

            existEvent.Location = Event.Location;

            var isExist = await _dbContext.Events.AnyAsync(x => x.Title.ToUpper() == Event.Title.ToUpper() && x.Id != id);

            if (isExist)
            {
                ModelState.AddModelError("Title", "Bu Kurs Artıq Mövcuddur");

                return View();
            }

            var trimmedName = existEvent.ImageUrl.Remove(0, 4);

            var pathForDelete = Path.Combine(Constants.ImagePath, trimmedName);

            if (Files.Exists(pathForDelete))
            {
                Files.Delete(pathForDelete);
            }

            var path = Path.Combine(Constants.ImagePath, "Event");

            var fileName = await Event.Image.GenerateFile(path);

            existEvent.ImageUrl = $"img/Event/{fileName}";

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }

}
