using StudentPortalAPI.Data;
using StudentPortalAPI.Models;
using StudentPortalAPI.Models.DTOs.Professor;

namespace ProfessorPortalAPI.Services.ProfessorService.Commands;

public class ProfessorCommandService
{
    private readonly IMapper _mapper;
    private readonly IDbContextFactory<DataContext> _contextFactory;

    public ProfessorCommandService(IMapper mapper, IDbContextFactory<DataContext> contextFactory)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    public async Task AddProfessor(AddProfessorDTO newProfessor)
    {
        if (newProfessor is null)
            throw new ArgumentNullException("There are missing fields.");

        using var _context = _contextFactory.CreateDbContext();

        if (await _context.Users.FirstOrDefaultAsync(Users => Users.Username == newProfessor.Username) is not null)
            throw new ArgumentException("Username already exists.");

        if (await _context.Users.FirstOrDefaultAsync(Users => Users.Email == newProfessor.Email) is not null)
            throw new ArgumentException("Email is already in use.");

        var professor = Professor.Create(newProfessor.Username, newProfessor.Password, newProfessor.Email, newProfessor.FirstName, newProfessor.LastName);

        _context.Professors.Add(professor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProfessor(UpdateProfessorDTO updatedProfessor)
    {
        using var _context = _contextFactory.CreateDbContext();
        var dbProfessor =
            await _context.Professors.FirstOrDefaultAsync(item => item.ProfessorId == updatedProfessor.ProfessorId)
            ?? throw new KeyNotFoundException("Professor not found.");

        _mapper.Map(updatedProfessor, dbProfessor);
        await _context.SaveChangesAsync();
    }

    public async Task AssignProfessorToCourse(AssignProfessorDTO assignProfessorDTO)
    {
        using var _context = _contextFactory.CreateDbContext();

        var course =
            await _context.Courses.FirstOrDefaultAsync(m => m.Id == assignProfessorDTO.CourseId)
            ?? throw new KeyNotFoundException("Course not found.");

        if (course.Professor != null)
            throw new Exception("Course already has a Professor assigned.");

        var professor = await _context.Professors.FirstOrDefaultAsync(m => m.ProfessorId == assignProfessorDTO.ProfessorId)
            ?? throw new KeyNotFoundException("Professor not found.");

        course.Professor = professor;
        await _context.SaveChangesAsync();
    }

    public async Task UnassignProfessorToCourse(AssignProfessorDTO assignProfessorDTO)
    {
        using var _context = _contextFactory.CreateDbContext();

        //var link =
        //    await _context.CourseEnrollments.FirstOrDefaultAsync(
        //        ma =>
        //            ma.ProfessorId == UnlinkCourseEnrollment.ProfessorId
        //            && ma.CourseId == UnlinkCourseEnrollment.CourseId
        //    ) ?? throw new KeyNotFoundException("Link not found.");

        //_context.CourseEnrollments.Remove(link);
        await _context.SaveChangesAsync();
    }
}
