using BackProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackProject.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Logos> Logos { get; set; }
        public DbSet<MainSlider> MainSlider { get; set; }
        public DbSet<ServiceArea> ServiceArea { get; set; }
        public DbSet<AboutArea> AboutArea { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<NoticeArea> NoticeAreas { get; set; }
        public DbSet<NoticeBoard> NoticeBoards { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<TestImonialArea> TestImonialArea { get; set; }
        public DbSet<Blogs> Blogs { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Subscribe> Subscribe { get; set; }
        public DbSet<Footer> Footer { get; set; }
        public DbSet<Informations> Informations { get; set; }
        public DbSet<UsefulLinks> UsefulLinks { get; set; }
        public DbSet<GetInTouch> GetInTouch { get; set; }
        public DbSet<SocialMediaIcons> SocialMediaIcons { get; set;}
        public DbSet<Header> Header { get; set; }
        public DbSet<Navigations> Navigations { get; set; }

    }
}
