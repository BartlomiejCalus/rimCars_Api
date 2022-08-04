using AutoMapper;
using Microsoft.EntityFrameworkCore;
using rimCars_Api.Entities;
using rimCars_Api.Models;

namespace rimCars_Api.Services
{
    public interface IRimService
    {
        IEnumerable<RimsDto> GetAllRim(int idSalon);
        public int AddRim(AddRimDto dto, int idSalon);
    }

    public class RimService : IRimService
    {
        private readonly SalonsDbContext _dbContext;
        private readonly IMapper _mapper;

        public RimService(SalonsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<RimsDto> GetAllRim(int idSalon)
        {
            var result = _dbContext
                .Salons
                .Include(s => s.Address)
                .Include(s => s.Rims)
                .FirstOrDefault(s => s.Id == idSalon);

            if (result == null)
                return null;

            var rims = result.Rims.ToList();

            var rimsDto = _mapper.Map<List<RimsDto>>(rims);

            return rimsDto;

        }

        public int AddRim(AddRimDto dto, int idSalon)
        {
            var rim = _mapper.Map<Rim>(dto);
            rim.IdSalons = idSalon;
            rim.Salon = _dbContext
                .Salons
                .FirstOrDefault(s => s.Id == idSalon);
            
            _dbContext
                .Rims
                .Add(rim);

            _dbContext.SaveChanges();

            return rim.Id;
        }
    }
}
