using HeladeriaAPI.Models.Salsa.Dto;
using HeladeriaAPI.Services;
using HeladeriaAPI.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HeladeriaAPI.Controllers
{
    [Route("api/salsas")]
    [ApiController]
    public class SalsaController : ControllerBase
    {
        private readonly SalsaServices _service;

        public SalsaController(SalsaServices service)
        {
            _service = service;
        }   

        [HttpGet]
        public async Task<ActionResult<List<AllSalsaDTO>>> Get()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var salsa = await _service.GetOneById(id);
                return Ok(salsa);
            }
            catch (HttpError ex)
            {
                return StatusCode((int)ex.StatusCode, new HttpMessage(ex.Message));
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateSalsaDTO dto)
        {
            try
            {
                var s = await _service.CreateOne(dto);
                return CreatedAtAction(nameof(Get), new { id = s.Id }, s);
            }
            catch
            {
                return StatusCode(500, new HttpMessage("Error al crear la salsa."));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateSalsaDTO dto)
        {
            try
            {
                var s = await _service.UpdateOneById(id, dto);
                return Ok(s);
            }
            catch (HttpError ex)
            {
                return StatusCode((int)ex.StatusCode, new HttpMessage(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteOneById(id);
                return Ok(new HttpMessage($"Salsa con ID = {id} eliminada."));
            }
            catch (HttpError ex)
            {
                return StatusCode((int)ex.StatusCode, new HttpMessage(ex.Message));
            }
        }
    }
}