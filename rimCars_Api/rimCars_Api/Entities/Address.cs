namespace rimCars_Api.Entities
{
    public class Address
    {
        public int Id { get; set; }

        public string City { get; set; }
        public string Street { get; set; }

        public virtual Salon Salon { get; set; }

    }
}
