using BackProject.DAL;
using BackProject.DAL.Entities;
using BackProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BackProject.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CourseController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var course = _dbContext.Courses.ToList();
            var subscribe = _dbContext.Subscribe.FirstOrDefault();

            CourseViewModel viewModel = new CourseViewModel
            {
                Courses = course,
                Subscribe = subscribe,
            };

            return View(viewModel);
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return BadRequest();

            var course = _dbContext.Courses.Find(id);

            if (course == null) return NotFound();

            return View(course);
        }

        public IActionResult Search(string searchedCoursesTitle)
        {
            var searchedCourses = _dbContext.Courses
                .Where(x => x.Title.ToLower().Contains(searchedCoursesTitle.ToLower()))
                .ToList();

            return PartialView("_SearchedCoursePartial", searchedCourses);
        }
    }
}
