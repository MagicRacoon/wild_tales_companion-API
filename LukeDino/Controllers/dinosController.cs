using LukeDino.Classes.Dtos;
using LukeDino.Entities;
using LukeDino.Services;
using Microsoft.AspNetCore.Mvc;

namespace LukeDino.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class dinosController(ILogger<dinosController> logger, IDinoService dinoService) : ControllerBase
    {
        /// <summary>
        /// Gets all dinos
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<DinoDto>>> GetAllDinosAsync()
        {
            try
            {
                var results = await dinoService.GetAllDinosAsync();
                return Ok(results);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }            
        }

        /// <summary>
        /// Gets a specific dino
        /// </summary>
        /// <param name="dinoId"></param>
        /// <returns></returns>
        [HttpGet("/{dinoId}")]
        public async Task<ActionResult<DinoDto>> GetDinoAsync(int dinoId)
        {
            try
            {
                var results = await dinoService.GetAllDinosAsync(dinoId);
                return Ok(results);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }            
        }

        /// <summary>
        /// Add a dino
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<ActionResult<DinoDto>> AddDinoAsync(DinoDto form)
        {
            try
            {
                await dinoService.AddDinoAsync(form);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message ?? e.Message);
            }
        }

    }
}