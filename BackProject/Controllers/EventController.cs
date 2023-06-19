using BackProject.DAL;
using BackProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BackProject.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _dbContext;

        public EventController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var events = _dbContext.Events.ToList();
            var subscribe = _dbContext.Subscribe.FirstOrDefault();

            EventViewModel viewModel = new EventViewModel
            {
                Events = events,
                Subscribe = subscribe,
            };

            return View(viewModel);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var events = _dbContext.Events.Find(id);

            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }
    }
}
