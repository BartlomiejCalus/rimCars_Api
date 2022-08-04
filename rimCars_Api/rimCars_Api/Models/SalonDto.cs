using rimCars_Api;

namespace rimCars_Api.Models
{
    public class SalonDto
    {
        public int Id { get; set; }

        public string nrTel { get; set; }
        public string mail { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public List<RimsDto> Rims { get; set; }

    }
}
