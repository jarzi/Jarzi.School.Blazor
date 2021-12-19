using Jarzi.School.Blazor.Server.Data;
using Jarzi.School.Blazor.Shared.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jarzi.School.Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolClassController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SchoolClassController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var schoolClasses = await _context.SchoolClasses.ToListAsync();
            return Ok(schoolClasses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var schoolClass = await _context.SchoolClasses.FirstOrDefaultAsync(s => s.Id == id);
            return Ok(schoolClass);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Shared.Model.SchoolClass schoolClass)
        {
            _context.Add(schoolClass);
            await _context.SaveChangesAsync();
            return Ok(schoolClass.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Shared.Model.SchoolClass schoolClass)
        {
            _context.Entry(schoolClass).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var schoolClass = new SchoolClass { Id = id };
            var students = await _context.Students.ToListAsync();
            foreach (var student in students.Where(s => s.SchoolClass != null && s.SchoolClass.Id == id))
            {
                student.SchoolClass = null;
            }
            _context.Entry(schoolClass).State = EntityState.Deleted;
            _context.Remove(schoolClass);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
