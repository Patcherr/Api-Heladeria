using AutoMapper;
using HeladeriaAPI.Config;
using HeladeriaAPI.Models.Estado;
using HeladeriaAPI.Models.Helado;
using HeladeriaAPI.Models.Helado.Dto;
using HeladeriaAPI.Models.Ingrediente;
using HeladeriaAPI.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace HeladeriaAPI.Services
{
    public class HeladoServices
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;
        private readonly IngredienteServices _ingredienteServices;
        private readonly EstadoServices _estadoServices;
        public HeladoServices(IMapper mapper, ApplicationDbContext db, IngredienteServices ingredienteServices, EstadoServices estadoServices)
        {
            _mapper = mapper;
            _db = db;
            _ingredienteServices = ingredienteServices;
            _estadoServices = estadoServices;
        }

        private async Task<Helado> GetOneByIdOrException(int id)
        {
            var helado = await _db.Helados
                .Where(h => h.Id == id)
                .Include(h => h.Estado)
                .Include(h => h.Ingredientes)
                .FirstOrDefaultAsync();

            if (helado == null)
            {
                throw new HttpError($"No se encontro el helado con ID = {id}", HttpStatusCode.NotFound);
            }

            return helado;
        }

        public async Task<List<AllHeladoDTO>> GetAll()
        {
            var heladosDb = await _db.Helados
                .Include(hel => hel.Estado)
                .Include(hel => hel.Ingredientes)
                .ToListAsync();

            var helados = heladosDb.Select(h => new AllHeladoDTO
            {
                Id = h.Id,
                Nombre = h.Nombre,
                Precio = h.Precio,
                Estado = h.Estado,
                Ingredientes = h.Ingredientes.Select(i => i.Nombre).ToList()
            }).ToList();
            return helados;         
        }

        public async Task<Helado> GetOneById(int id)
        {
            return await GetOneByIdOrException(id);
        }

        public async Task<Helado> CreateOne(CreateHeladoDTO helado) {

            var h = _mapper.Map<Helado>(helado);
            var estado = await _estadoServices.GetOneByName("Pendiente");
            h.Estado = estado;

            if (helado.IngredientesIds != null && helado.IngredientesIds.Any())
            {
                var ingredientes = await _ingredienteServices.GetAllByIds(helado.IngredientesIds);
                h.Ingredientes = ingredientes;
            }

            await _db.Helados.AddAsync(h);
            await _db.SaveChangesAsync();
            return h;
        }

        public async Task<Helado> UpdateOneById(int id, UpdateHeladoDTO helado)
        {
            var heladoToUpdate = await GetOneByIdOrException(id);
           
           var heladoUpdated = _mapper.Map(helado, heladoToUpdate);

            if (helado.IngredientesIds != null && helado.IngredientesIds.Any())
            {
                var ingredientes = await _ingredienteServices.GetAllByIds(helado.IngredientesIds);
                heladoUpdated.Ingredientes = ingredientes;
            }

            _db.Helados.Update(heladoUpdated);
            await _db.SaveChangesAsync();

            return heladoToUpdate;
        }

        public async Task DeleteOneById(int id)
        {
            var helado = await GetOneByIdOrException(id);
            _db.Helados.Remove(helado);
            await _db.SaveChangesAsync();
            if (await _db.Helados.AnyAsync(hel => hel.Id == id))
            {
                throw new HttpError($"No se pudo eliminar el helado con ID = {id}", HttpStatusCode.InternalServerError);
            }
        }
          
    }
}
