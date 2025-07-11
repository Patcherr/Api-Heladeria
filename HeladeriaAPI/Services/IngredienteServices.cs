using AutoMapper;
using HeladeriaAPI.Config;
using HeladeriaAPI.Models.Estado;
using HeladeriaAPI.Models.Helado;
using HeladeriaAPI.Models.Helado.Dto;
using HeladeriaAPI.Models.Ingrediente;
using HeladeriaAPI.Models.Ingrediente.Dto;
using HeladeriaAPI.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace HeladeriaAPI.Services
{
    public class IngredienteServices
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;
        public IngredienteServices(IMapper mapper, ApplicationDbContext db)
        {
            _mapper = mapper;
            _db = db;
        }

        private async Task<Ingrediente> GetOneByIdOrException(int id)
        {
            var ingrediente = await _db.Ingredientes.Where(h => h.Id == id).FirstOrDefaultAsync();

            if (ingrediente == null)
            {
                throw new HttpError($"No se encontro el Ingrediente con ID = {id}", HttpStatusCode.NotFound);
            }

            return ingrediente;
        }

        public async Task<List<Ingrediente>> GetAll()
        {
            var ingredientes = await _db.Ingredientes.ToListAsync();
            return ingredientes;
        }

        public async Task<List<Ingrediente>> GetAllByIds(List<int> ingredienteIds)
        {
            if (ingredienteIds == null || !ingredienteIds.Any())
            {
                throw new HttpError("La lista de IDs de Ingredientes no puede estar vacía.", HttpStatusCode.BadRequest);
            }
            var ingredientes = await _db.Ingredientes.Where(i => ingredienteIds.Contains(i.Id)).ToListAsync();
            return ingredientes;
        }

        public async Task<Ingrediente> GetOneById(int id)
        {
            return await GetOneByIdOrException(id);
        }

        public async Task<Ingrediente> GetOneByName(string nombre)
        {
            var ingrediente = await _db.Ingredientes.FirstOrDefaultAsync(e => e.Nombre == nombre);
            if (ingrediente == null)
            {
                throw new HttpError($"No se encontro el Ingrediente con Nombre = {nombre}", HttpStatusCode.NotFound);
            }
            return ingrediente;
        }

        public async Task<Ingrediente> CreateOne(CreateIngredienteDTO ingrediente)
        {

            var ing = _mapper.Map<Ingrediente>(ingrediente);

            await _db.Ingredientes.AddAsync(ing);
            await _db.SaveChangesAsync();
            return ing;
        }

        public async Task<Ingrediente> UpdateOneById(int id, UpdateIngredienteDTO ingrediente)
        {
            var ingredienteToUpdate = await GetOneByIdOrException(id);
            

            var ingredienteUpdated = _mapper.Map(ingrediente, ingredienteToUpdate);

            _db.Ingredientes.Update(ingredienteUpdated);
            await _db.SaveChangesAsync();

            return ingredienteUpdated;
        }

        public async Task DeleteOneById(int id)
        {
            var ingrediente = await GetOneByIdOrException(id);
            _db.Ingredientes.Remove(ingrediente);
            await _db.SaveChangesAsync();
            if (await _db.Ingredientes.AnyAsync(hel => hel.Id == id))
            {
                throw new HttpError($"No se pudo eliminar el Ingrediente con ID = {id}", HttpStatusCode.InternalServerError);
            }
        }
    }
}
