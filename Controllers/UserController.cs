using Microsoft.AspNetCore.Mvc;

namespace UserPortalAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    //private readonly IUserService _userService;

    //public UserController(IUserService userService)
    //{
    //    this._userService = userService;
    //}

    //// GET: api/<UserController>
    //[HttpGet]
    //public async Task<ActionResult<ServiceResponse<List<GetUserDTO>>>> Get()
    //{
    //    return Ok(await _userService.GetAllUsers());
    //}

    //// GET api/<UserController>/5
    //[HttpGet("{id}")]
    //public async Task<ActionResult<ServiceResponse<GetUserDTO>>> GetSingle(int id)
    //{
    //    try
    //    {
    //        var response = await _userService.GetUserById(id);
    //        return Ok(response);
    //    }
    //    catch (KeyNotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(500, ex.Message);
    //    }
    //}

    //// POST api/<UserController>
    //[HttpPost]
    //public async Task<ActionResult> AddUser(
    //    AddUserDTO novoUser
    //)
    //{
    //    try
    //    {
    //        var response = await _userService.AddUser(novoUser);
    //        return NoContent();
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(500, ex.Message);
    //    }
    //}

    //[HttpPost("Enroll/")]
    //public async Task<ActionResult> LinkUserToCourse(
    //    EnrollUserDTO newLinkCourseUser
    //)
    //{
    //    try
    //    {
    //        await _userService.LinkUserToCourse(newLinkCourseUser);
    //        return NoContent();
    //    }
    //    catch (KeyNotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (ArgumentException ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //    catch (ApplicationException ex)
    //    {
    //        return StatusCode(400, ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(500, ex.Message);
    //    }
    //}

    //[HttpPost("Disenroll/")]
    //public async Task<ActionResult> UnlinkUserToCourse(EnrollUserDTO unlinkCourseUser)
    //{
    //    try
    //    {
    //        await _userService.UnlinkUserToCourse(unlinkCourseUser);
    //        return NoContent();
    //    }
    //    catch (KeyNotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(500, ex.Message);
    //    }
    //}

    //// PUT api/<UserController>/5
    //[HttpPut("{id}")]
    //public async Task<ActionResult> UpdateUser(
    //    UpdateUserDTO user,
    //    int id
    //)
    //{
    //    try
    //    {
    //        var response = await _userService.UpdateUser(user, id);
    //        return NoContent();
    //    }
    //    catch (KeyNotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(500, ex.Message);
    //    }
    //}

    //// DELETE api/<UserController>/5
    //[HttpDelete("{id}")]
    //public async Task<ActionResult> DeleteUser(int id)
    //{
    //    try
    //    {
    //        var response = await _userService.DeleteUser(id);
    //        return NoContent();
    //    }
    //    catch (KeyNotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(500, ex.Message);
    //    }
    //}
}
