using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Website.Models.ModelVM
{
    public class SkillVM
    {
        public int Id { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public string Name { get; set; }
    }
}
