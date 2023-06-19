namespace BackProject.DAL.Entities
{
    public class Footer : Entity
    {
        public string Description { get; set; }
        public string Creator { get; set; }
        public ICollection<Logos> Logos { get; set; }
        public ICollection<SocialMediaIcons> SocialMediaIcons { get; set; }
        public ICollection<Informations> Informations { get; set; }
        public ICollection<UsefulLinks> UsefulLinks { get; set; }
        public ICollection<GetInTouch> GetInTouch { get; set; }
    }
}
