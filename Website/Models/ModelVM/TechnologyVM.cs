using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Website.Models.ModelVM
{
    public class TechnologyVM
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public IFormFile image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
