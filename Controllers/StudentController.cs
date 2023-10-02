using Microsoft.AspNetCore.Mvc;
using StudentPortalAPI.Models;
using StudentPortalAPI.Models.DTOs.Student;
using StudentPortalAPI.Services.StudentService;

namespace StudentPortalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        this._studentService = studentService;
    }

    // GET: api/<StudentController>
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<GetStudentDTO>>>> GetAll()
    {
        return Ok(await _studentService.GetAllStudents());
    }

    // GET api/<StudentController>/5
    [HttpGet("{username}")]
    public async Task<ActionResult<ServiceResponse<GetStudentDTO>>> GetSingle(string username)
    {
        try
        {
            var response = await _studentService.GetStudentByUsername(username);
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

    // POST api/<StudentController>
    [HttpPost]
    public async Task<ActionResult> AddStudent(AddStudentDTO novoStudent)
    {
        try
        {
            await _studentService.AddStudent(novoStudent);
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

    [HttpPost("Enroll/")]
    public async Task<ActionResult> LinkStudentToCourse(EnrollStudentDTO newLinkCourseStudent)
    {
        try
        {
            await _studentService.LinkStudentToCourse(newLinkCourseStudent);
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

    [HttpPost("Disenroll/")]
    public async Task<ActionResult> UnlinkStudentToCourse(EnrollStudentDTO unlinkCourseStudent)
    {
        try
        {
            await _studentService.UnlinkStudentToCourse(unlinkCourseStudent);
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

    // PUT api/<StudentController>/5
    [HttpPut]
    public async Task<ActionResult> UpdateStudent(UpdateStudentDTO student)
    {
        try
        {
            await _studentService.UpdateStudent(student);
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
