using AutoMapper;
using Microsoft.EntityFrameworkCore;
using rimCars_Api.Entities;
using rimCars_Api.Models;

namespace rimCars_Api.Services
{
    public interface ISalonService
    {
        int Add(AddSalonDto dto);
        IEnumerable<SalonDto> GetAll();
        SalonDto GetOne(int id);

        public bool Delete(int id);
    }

    public class SalonService : ISalonService
    {
        private readonly SalonsDbContext _dbContext;
        private readonly IMapper _mapper;

        public SalonService(SalonsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public SalonDto GetOne(int id)
        {
            var result = _dbContext
                .Salons
                .Include(s => s.Address)
                .Include(s => s.Rims)
                .FirstOrDefault(r => r.Id == id);

            if (result == null)
                return null;

            var salonDto = _mapper.Map<SalonDto>(result);

            return salonDto;
        }

        public IEnumerable<SalonDto> GetAll()
        {
            var result = _dbContext
                .Salons
                .Include(s => s.Address)
                .Include(s => s.Rims)
                .ToList();

            var salonsDto = _mapper.Map<List<SalonDto>>(result);

            return salonsDto;
        }

        public int Add(AddSalonDto dto)
        {
            var salon = _mapper.Map<Salon>(dto);
            _dbContext.Salons.Add(salon);
            _dbContext.SaveChanges();

            return salon.Id;
        }

        public bool Delete(int id)
        {
            var result = _dbContext
                .Salons
                .FirstOrDefault(r => r.Id == id);
            if (result == null)
                return false;

            _dbContext.Salons.Remove(result);
            _dbContext.SaveChanges();
            return true;

        }

        
    }
}
