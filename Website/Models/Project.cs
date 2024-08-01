using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; }
        public string videourl { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
        public string Code { get; set; }
        public int TechnoId { get; set; }
        [ForeignKey(nameof(TechnoId))]
        public Technology Techno { get; set; }
        
    }
}
