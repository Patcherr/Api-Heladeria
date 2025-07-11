using HeladeriaAPI.Models.Helado;
using HeladeriaAPI.Models.Helado.Dto;
using HeladeriaAPI.Models.Ingrediente;
using HeladeriaAPI.Models.Ingrediente.Dto;
using HeladeriaAPI.Services;
using HeladeriaAPI.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeladeriaAPI.Controllers
{
    [Route("api/ingredientes")]
    [ApiController]
    public class IngredienteController : ControllerBase
    {
        private readonly IngredienteServices _ingredienteServices;

        public IngredienteController(IngredienteServices ingredienteServices)
        {
            _ingredienteServices = ingredienteServices;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpMessage))]
        public async Task<ActionResult<List<Ingrediente>>> GetIngredientes()
        {
            try
            {
                var ingredientes = await _ingredienteServices.GetAll();
                return Ok(ingredientes);
            }
            catch (HttpError ex)
            {
                return StatusCode((int)ex.StatusCode, new HttpMessage(ex.Message));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new HttpMessage("Algo salio mal"));
            }
        }

        //[HttpPost("/ids")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpMessage))]
        //public async Task<ActionResult<List<Ingrediente>>> GetIngredientesIds([FromBody]List<int> ids)
        //{
        //    try
        //    {
        //        var ingredientes = await _ingredienteServices.GetAllByIds(ids);
        //        return Ok(ingredientes);
        //    }
        //    catch(HttpError ex)
        //    {
        //        return StatusCode((int)ex.StatusCode, new HttpMessage(ex.Message));
        //    }
        //    catch
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new HttpMessage("Algo salio mal"));
        //    }
        //}

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(HttpMessage))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpMessage))]
        public async Task<ActionResult<Ingrediente>> GetIngrediente(int id)
        {
            try
            {
                return await _ingredienteServices.GetOneById(id);
            }
            catch (HttpError ex)
            {
                return StatusCode((int)ex.StatusCode, new HttpMessage(ex.Message));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new HttpMessage("Algo salio mal"));
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Helado))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpMessage))]
        public async Task<ActionResult> Create([FromBody] CreateIngredienteDTO ingrediente)
        {
            try
            {
                var ingredienteCreated = await _ingredienteServices.CreateOne(ingrediente);
                return Created("api/helados", ingredienteCreated);
            }
            catch (HttpError ex)
            {
                return StatusCode((int)ex.StatusCode, new HttpMessage(ex.Message));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new HttpMessage("Algo salio mal creando el Ingrediente"));
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Helado))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(HttpMessage))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpMessage))]
        public async Task<ActionResult<Ingrediente>> Update(int id, [FromBody] UpdateIngredienteDTO ingrediente)
        {
            try
            {
                return await _ingredienteServices.UpdateOneById(id, ingrediente);
            }
            catch (HttpError ex)
            {
                return StatusCode((int)ex.StatusCode, new HttpMessage(ex.Message));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new HttpMessage($"Algo salio mal actualizando el Ingrediente con ID = {id}"));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(HttpMessage))]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _ingredienteServices.DeleteOneById(id);
                return Ok(new HttpMessage($"Ingrediente con ID = {id} eliminado correctamente."));
            }
            catch (HttpError ex)
            {
                return StatusCode((int)ex.StatusCode, new HttpMessage(ex.Message));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new HttpMessage($"Algo salio mal eliminando el Ingrediente con ID = {id}"));
            }
        }
    }
}
