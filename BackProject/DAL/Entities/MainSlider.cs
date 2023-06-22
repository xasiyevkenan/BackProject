using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackProject.DAL.Entities
{
    public class MainSlider : Entity
    {
        [MaxLength(300)]
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        [MaxLength(600)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
