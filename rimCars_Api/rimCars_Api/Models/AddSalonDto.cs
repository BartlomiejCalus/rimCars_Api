using System.ComponentModel.DataAnnotations;

namespace rimCars_Api.Models
{
    public class AddSalonDto
    {
        [Required]
        [Phone]
        public string nrTel { get; set; }
        [EmailAddress]
        public string mail { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
    }
}
