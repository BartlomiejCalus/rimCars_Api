namespace rimCars_Api.Entities
{
    public class Salon
    {
        public int Id { get; set; }
        public string nrTel { get; set; }
        public string mail { get; set; }

        public int addressId { get; set; }
        public virtual Address Address { get; set; } 
        public virtual List <Rim> Rims { get; set; }
    }
}
