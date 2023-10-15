using Microsoft.AspNetCore.Mvc;
using ProfessorPortalAPI.Services.ProfessorService;
using StudentPortalAPI.Models;
using StudentPortalAPI.Models.DTOs.Professor;

namespace ProfessorPortalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfessorController : ControllerBase
{
    private readonly IProfessorService _professorService;

    public ProfessorController(IProfessorService professorService)
    {
        _professorService = professorService;
    }

    // GET: api/<ProfessorController>
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<GetProfessorDTO>>>> GetAll()
    {
        return Ok(await _professorService.GetAllProfessors());
    }

    // GET api/<ProfessorController>/5
    [HttpGet("{username}")]
    public async Task<ActionResult<ServiceResponse<GetProfessorDTO>>> GetSingle(string username)
    {
        try
        {
            var response = await _professorService.GetProfessorByUsername(username);
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // POST api/<ProfessorController>
    [HttpPost]
    public async Task<ActionResult> AddProfessor(AddProfessorDTO newProfessor)
    {
        try
        {
            await _professorService.AddProfessor(newProfessor);
            return NoContent();
        }
        catch (ArgumentNullException ex)
        {
            return StatusCode(500, ex.Message);
        }
        catch (ArgumentException ex)
        {
            return StatusCode(500, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("Assign/")]
    public async Task<ActionResult> AssignProfessorToCourse(AssignProfessorDTO newAssignProfessor)
    {
        try
        {
            await _professorService.AssignProfessorToCourse(newAssignProfessor);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(400, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("Unassign/")]
    public async Task<ActionResult> UnassignProfessorToCourse(AssignProfessorDTO unassignCourseProfessor)
    {
        try
        {
            await _professorService.UnassignProfessorToCourse(unassignCourseProfessor);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // PUT api/<ProfessorController>/5
    [HttpPut]
    public async Task<ActionResult> UpdateProfessor(UpdateProfessorDTO professor)
    {
        try
        {
            await _professorService.UpdateProfessor(professor);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }



}

