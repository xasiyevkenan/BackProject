using BackProject.DAL;
using BackProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BackProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ContactController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var contact = _dbContext.Contact.ToList();
            var subscribe = _dbContext.Subscribe.FirstOrDefault();

            ContactViewModel viewModel = new ContactViewModel
            {
                Contact = contact,
                Subscribe = subscribe,
            };

            return View(viewModel);
        }
    }
}
