using Microsoft.AspNetCore.Mvc;
using StudentPortalAPI.Models;
using StudentPortalAPI.Models.DTOs.Course;
using StudentPortalAPI.Services.CourseService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentPortalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    // GET: api/<CourseController>
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<GetCourseDTO>>>> Get()
    {
        try
        {
            var response = await _courseService.GetAllCourses();
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

    // GET api/<CourseController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetCourseDTO>>> GetSingle(int id)
    {
        try
        {
            var response = await _courseService.GetCourseById(id);
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

    [HttpGet("Professor/{id}")]
    public async Task<ActionResult<ServiceResponse<GetCourseDTO>>> GetByProfessorId(int id)
    {
        try
        {
            var response = await _courseService.GetCoursesByProfessorId(id);
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

    // POST api/<CourseController>
    [HttpPost]
    public async Task<ActionResult> AddCourse(AddCourseDTO newCourse)
    {
        try
        {
            await _courseService.AddCourse(newCourse);
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

    // PUT api/<CourseController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCourse(UpdateCourseDTO updatedCourse, int id)
    {
        try
        {
            await _courseService.UpdateCourse(updatedCourse, id);
            return NoContent();
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
        try
        {
            await _courseService.DeleteCourse(id);
            return NoContent();
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
}

