namespace BackProject.DAL.Entities
{
    public class Header : Entity
    {
        public Logos Logo { get; set; }
        public ICollection<Navigations> Navigations { get; set; }

    }
}
