using BackProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackProject.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public FooterViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var footer = _context.Footer.Include(x => x.GetInTouch).Include(x => x.SocialMediaIcons).Include(x => x.Informations).Include(x => x.UsefulLinks).Include(x => x.Logos).FirstOrDefault();

            return View(footer);
        }
    }
}
