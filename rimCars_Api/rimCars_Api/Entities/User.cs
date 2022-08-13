namespace rimCars_Api.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHs { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Company { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
