namespace BackProject.DAL.Entities
{
    public class NoticeArea : Entity
    {
        public string VideoUrl { get; set; }
        public string VideoIcon { get; set; }
        public ICollection<NoticeBoard> NoticeBoards { get; set;}
    }
}
