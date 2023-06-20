using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackProject.DAL.Entities
{
    public class Blogs : Entity
    {
        public string BlogTop { get; set; }
        [MaxLength(300)]
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public string Description { get; set; }
    }
}
