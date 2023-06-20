using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackProject.DAL.Entities
{
    public class Events : Entity
    {
        [MaxLength(300)]
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public string Date { get; set; }
        [MaxLength(50)]
        public string Time { get; set; }
        [MaxLength(30)]
        public string Location { get; set; }
        [MaxLength(300)]
        public string Title { get; set; }
    }
}
