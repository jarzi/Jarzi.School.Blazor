using Jarzi.School.Blazor.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jarzi.School.Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var students = await _context.Students.Include(s => s.SchoolClass).ToListAsync();
            foreach (var student in students.Where(student => student.SchoolClass != null))
            {
                student.SchoolClass!.Students = null;
                student.SchoolClass.Teachers = null;
            }
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var student = await _context.Students.Include(s => s.SchoolClass).FirstOrDefaultAsync(s => s.Id == id);
            if (student?.SchoolClass != null) student.SchoolClass.Students = null;
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Shared.Model.Student student)
        {
            if (student?.SchoolClass != null) _context.Entry(student.SchoolClass).State = EntityState.Modified;
            _context.Entry(student).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return Ok(student.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Shared.Model.Student student)
        {
            _context.Entry(student.SchoolClass).State = EntityState.Modified;
            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = new Shared.Model.Student { Id = id };
            _context.Remove(student);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
