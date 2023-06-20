using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackProject.DAL.Entities
{
    public class Teachers : Entity
    {
        [MaxLength(300)]
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public string Name { get; set; }
        public string Degree { get; set; }
    }
}
