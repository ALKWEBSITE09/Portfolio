using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Website.Models
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        [AllowNull]
        public string Photo { get; set; }
        [AllowNull]
        public string aboutphoto { get; set; }
        public string Name { get; set; }
        public DateOnly Dob { get; set; }
        public string ProfileDesc { get; set; }
        public string MainDomain { get; set; }
        public string district { get; set; }
        public string Country { get; set; }
        public string state { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string zipcode { get; set; }
        public string LogoName { get; set; }

    }
}
