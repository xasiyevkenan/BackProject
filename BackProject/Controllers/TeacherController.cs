using BackProject.DAL;
using BackProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackProject.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AppDbContext _dbContext;

        public TeacherController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var teachers = _dbContext.Teachers.ToList();
            var subscribe = _dbContext.Subscribe.FirstOrDefault();

            TeacherViewModel viewModel = new TeacherViewModel
            {
                Teachers = teachers,
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

            var teachers = _dbContext.Teachers.Find(id);

            if (teachers == null)
            {
                return NotFound();
            }

            return View(teachers);
        }
    }
}
