using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackProject.DAL.Entities
{
    public class Courses : Entity
    {
        public string Description { get; set; }
        [MaxLength(300)]
        public string Title { get; set; }
        [MaxLength(300)]
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
