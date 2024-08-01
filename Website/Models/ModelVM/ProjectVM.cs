using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Website.Models.ModelVM
{
    public class ProjectVM
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public string videourl { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
        public string Code { get; set; }
        public int TechnoId { get; set; }
        
    }
}
