using BackProject.DAL.Entities;
using BackProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            var isExist = await _dbContext.Events.AnyAsync(x => x.Title.ToUpper() == Event.Title.ToUpper());

            if (isExist)
            {
                ModelState.AddModelError("Title", "Bu Event Artıq Mövcuddur");
                return View();
            }

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

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }

}
