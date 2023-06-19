namespace BackProject.DAL.Entities
{
    public class Events : Entity
    {
        public string ImageUrl { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Location { get; set; }
        public string Title { get; set; }
    }
}
