using BackProject.DAL.Entities;

namespace BackProject.ViewModels
{
    public class AboutViewModel
    {
        public AboutArea About { get; set; }
        public List<Teachers> Teachers { get; set; }
        public TestImonialArea Test { get; set; }
        public NoticeArea Notice { get; set; }
        public List<NoticeBoard> NoticeBoards { get; set;}
        public Subscribe Subscribe { get; set; }
        public Logos Logos { get; set; }
    }
}
