namespace rimCars_Api.Entities
{
    public class Rim
    {
        public int Id { get; set; }

        public int size { get; set; }
        public string color { get; set; }
        public double prize { get; set; }

        public int IdSalons { get; set; }
        public virtual Salon Salon { get; set; }
    }
}
