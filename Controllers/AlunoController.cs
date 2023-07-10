﻿using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PortalNotas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _alunoService;
        public AlunoController(IAlunoService alunoService)
        {
            this._alunoService = alunoService;
        }

        // GET: api/<AlunoController>
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetAlunoDTO>>>> Get()
        {
            return Ok(await _alunoService.GetAllAlunos());
        }

        // GET api/<AlunoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetAlunoDTO>>> GetSingle(int id)
        {
            var response = await _alunoService.GetAlunoById(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // POST api/<AlunoController>
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetAlunoDTO>>> AddAluno(AddAlunoDTO novoAluno)
        {
            try
            {
                var response = await _alunoService.AddAluno(novoAluno);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }

        [HttpPost("Link/")]
        public async Task<ActionResult<ServiceResponse<GetAlunoDTO>>> LinkAlunoToMateria(LinkMateriaAlunoDTO newLinkMateriaAluno)
        {
            try
            {
                await _alunoService.LinkAlunoToMateria(newLinkMateriaAluno);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex is KeyNotFoundException)
                    return NotFound(ex.Message);
                if (ex is ArgumentException argumentException)
                    return BadRequest(argumentException.Message);
                if (ex is ApplicationException applicationException)
                    return StatusCode(400, applicationException.Message);
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("Unlink/")]
        public async Task<ActionResult> UnlinkAlunoToMateria(LinkMateriaAlunoDTO unlinkMateriaAluno)
        {
            try
            {
                await _alunoService.UnlinkAlunoToMateria(unlinkMateriaAluno);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex is KeyNotFoundException)
                    return NotFound(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<AlunoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetAlunoDTO>>> UpdateAluno(UpdateAlunoDTO aluno, int id)
        {
            var response = await _alunoService.UpdateAluno(aluno, id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // DELETE api/<AlunoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAluno(int id)
        {
            var response = await _alunoService.DeleteAluno(id);
            if (response.Success == false)
            {
                return NotFound(response);
            }
            return NoContent();
        }
    }
}
