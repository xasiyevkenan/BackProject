using System.Security.Policy;

namespace BackProject.DAL.Entities
{
    public class Navigations : Entity
    {
        public string Title { get; set; }
        public bool IsMain { get; set; }
        public int? ParentNavigationId { get; set; }
        public Navigations ParentNavigation { get; set; }
    }
}
