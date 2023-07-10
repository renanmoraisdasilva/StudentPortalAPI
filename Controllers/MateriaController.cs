using Microsoft.AspNetCore.Mvc;
using PortalNotas.Models.DTOs.Materia;
using PortalNotas.Services.MateriaService;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PortalNotas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        private readonly IMateriaService _materiaService;
        public MateriaController(IMateriaService materiaService)
        {
            this._materiaService = materiaService;
        }

        // GET: api/<MateriaController>
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetMateriaFromAlunoDTO>>>> Get()
        {
            return Ok(await _materiaService.GetAllMaterias());
        }

        // GET api/<MateriaController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetMateriaFromAlunoDTO>>> GetSingle(int id)
        {
            var response = await _materiaService.GetMateriaById(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // POST api/<MateriaController>
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetMateriaFromAlunoDTO>>>> AddMateria(AddMateriaDTO novoMateria)
        {
            return Ok(await _materiaService.AddMateria(novoMateria));
        }

        // PUT api/<MateriaController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetMateriaFromAlunoDTO>>> UpdateMateria(UpdateMateriaDTO aluno, int id)
        {
            var response = await _materiaService.UpdateMateria(aluno, id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // DELETE api/<MateriaController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMateria(int id)
        {
            var response = await _materiaService.DeleteMateria(id);
            if (response.Success == false)
            {
                return NotFound(response);
            }
            return NoContent();
        }
    }
}
