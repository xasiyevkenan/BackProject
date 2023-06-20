using BackProject.DAL.Entities;
using BackProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackProject.Areas.AdminPanel.Controllers
{
    public class TeacherController : AdminController
    {

        private readonly AppDbContext _dbContext;

        public TeacherController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var teachers = await _dbContext.Teachers.ToListAsync();

            return View(teachers);
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

            var isExist = await _dbContext.Teachers.AnyAsync(x => x.Name.ToUpper() == teacher.Name.ToUpper());

            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu Müəllim Artıq Mövcuddur");
                return View();
            }

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

            var isExist = await _dbContext.Teachers.AnyAsync(x => x.Name.ToUpper() == teacher.Name.ToUpper() && x.Id != id);

            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu Müəllim Artıq Mövcuddur");
                return View();
            }

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }

}
