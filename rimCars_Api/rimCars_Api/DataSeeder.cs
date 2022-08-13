using rimCars_Api.Entities;

namespace rimCars_Api
{
    public class DataSeeder
    {
        private readonly SalonsDbContext _dbContext;

        public DataSeeder(SalonsDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public void seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Salons.Any())
                {
                    var salons = GetSalons();
                    _dbContext.Salons.AddRange(salons);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };
            return roles;
        }

        private IEnumerable<Salon> GetSalons()
        {
            var salons = new List<Salon>()
            {
                new Salon()
                {
                    nrTel = "123654789",
                    mail = "mymail@gamil.com",

                    Address = new Address()
                    {
                        City = "Częstochowa",
                        Street = "Wolna 44",
                    },

                    Rims = new List<Rim>()
                    {
                        new Rim()
                        {
                            size = 15,
                            color = "moonnight black",
                            prize = 624.50
                        },
                        new Rim()
                        {
                            size = 17,
                            color = "black and red",
                            prize = 705.25
                        }
                    }

                },
                new Salon()
                {
                    nrTel = "987456312",
                    mail = "yourmail@gamil.com",

                    Address = new Address()
                    {
                        City = "Gliwice",
                        Street = "Pszczyńska 44",
                    },

                    Rims = new List<Rim>()
                    {
                        new Rim()
                        {
                            size = 16,
                            color = "moonnight black",
                            prize = 624.50
                        },
                        new Rim()
                        {
                            size = 17,
                            color = "sea blue",
                            prize = 705.25
                        }
                    }

                }
            };
            return salons;
        }


    }
}
