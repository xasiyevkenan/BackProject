using BackProject.DAL.Entities;

namespace BackProject.ViewModels
{
    public class HomeViewModel
    {
        public List<MainSlider> MainSlider { get; set; }
        public List<ServiceArea> ServiceArea { get; set; }
        public AboutArea AboutArea { get; set; }
        public List<Courses> Courses { get; set; }
        public NoticeArea NoticeArea { get; set; }
        public List<NoticeBoard> NoticeBoard { get; set; }
        public List<Events> Events { get; set; }
        public TestImonialArea TestImonialArea { get; set; }
        public List<Blogs> Blogs { get; set; }
        public Logos Logos { get; set; }
        public Subscribe Subscribe { get; set; }
    }
}
