using Microsoft.AspNetCore.Mvc;
using StudentPortalAPI.Models;
using StudentPortalAPI.Models.DTOs.Course;
using StudentPortalAPI.Models.DTOs.Student;
using StudentPortalAPI.Services.CourseService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentPortalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly ICourseService _materiaService;

    public CourseController(ICourseService materiaService)
    {
        this._materiaService = materiaService;
    }

    // GET: api/<CourseController>
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<GetCourseFromStudentDTO>>>> Get()
    {
        var response = await _materiaService.GetAllCourses();
        if (response.Data is null)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    // GET api/<CourseController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetCourseFromStudentDTO>>> GetSingle(int id)
    {
        var response = await _materiaService.GetCourseById(id);
        if (response.Data is null)
            return NotFound(response);
        return Ok(response);
    }

    // POST api/<CourseController>
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<GetCourseFromStudentDTO>>>> AddCourse(
        AddCourseDTO novoCourse
    )
    {
        try
        {
            var response = await _materiaService.AddCourse(novoCourse);
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // PUT api/<CourseController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult<ServiceResponse<GetCourseFromStudentDTO>>> UpdateCourse(
        UpdateCourseDTO aluno,
        int id
    )
    {
        try
        {
            var response = await _materiaService.UpdateCourse(aluno, id);
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // DELETE api/<CourseController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCourse(int id)
    {
        var response = await _materiaService.DeleteCourse(id);
        if (response.Success == false)
            return NotFound(response);
        return NoContent();
    }
}

