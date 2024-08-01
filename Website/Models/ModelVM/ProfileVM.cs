using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Website.Models.ModelVM
{
    public class ProfileVM
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        [AllowNull]
        public IFormFile Photo { get; set; }
        [NotMapped]
        [AllowNull]
        public IFormFile aboutphoto { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateOnly Dob { get; set; }
        public string ProfileDesc { get; set; }
        public string MainDomain { get; set; }
        public string Country { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
