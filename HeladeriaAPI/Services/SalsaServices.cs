using AutoMapper;
using HeladeriaAPI.Config;
using HeladeriaAPI.Models.Salsa;
using HeladeriaAPI.Models.Salsa.Dto;
using HeladeriaAPI.Utils;
using HeladeriaAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace HeladeriaAPI.Services
{
    public class SalsaServices
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;
        private readonly IngredienteServices _ingredienteServices;

        public SalsaServices(IMapper mapper, ApplicationDbContext db, IngredienteServices ingredienteServices)
        {
            _mapper = mapper;
            _db = db;
            _ingredienteServices = ingredienteServices;
        }

        private async Task<Salsa> GetOneByIdOrException(int id)
        {
            var salsa = await _db.Salsas
                .Include(s => s.Ingredientes)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (salsa == null)
                throw new HttpError($"No se encontró la salsa con ID = {id}", HttpStatusCode.NotFound);

            return salsa;
        }

        public async Task<List<AllSalsaDTO>> GetAll()
        {
            var salsas = await _db.Salsas.Include(s => s.Ingredientes).ToListAsync();
            return _mapper.Map<List<AllSalsaDTO>>(salsas);
        }
        public async Task<Salsa> GetOneById(int id)
        {
            return await GetOneByIdOrException(id);
        }

        public async Task<Salsa> CreateOne(CreateSalsaDTO dto)
        {
            var nueva = _mapper.Map<Salsa>(dto);

            if (dto.IngredientesIds != null && dto.IngredientesIds.Any())
            {
                var ingredientes = await _ingredienteServices.GetAllByIds(dto.IngredientesIds);
                nueva.Ingredientes = ingredientes;
            }

            await _db.Salsas.AddAsync(nueva);
            await _db.SaveChangesAsync();

            return nueva;
        }

        public async Task<Salsa> UpdateOneById(int id, UpdateSalsaDTO dto)
        {
            var salsa = await GetOneByIdOrException(id);

            var actualizada = _mapper.Map(dto, salsa);

            if (dto.IngredientesIds != null)
            {
                var ingredientes = await _ingredienteServices.GetAllByIds(dto.IngredientesIds);
                actualizada.Ingredientes = ingredientes;
            }

            _db.Salsas.Update(actualizada);
            await _db.SaveChangesAsync();

            return actualizada;
        }

        public async Task DeleteOneById(int id)
        {
            var salsa = await GetOneByIdOrException(id);
            _db.Salsas.Remove(salsa);
            await _db.SaveChangesAsync();
        }
    }
}
