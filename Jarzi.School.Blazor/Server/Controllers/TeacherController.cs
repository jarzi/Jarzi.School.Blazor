using Jarzi.School.Blazor.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jarzi.School.Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var teachers = await _context.Teachers.Include(s => s.SchoolClasses).ToListAsync();
            foreach (var teacherSchoolClass in teachers.SelectMany(teacher => teacher.SchoolClasses))
            {
                teacherSchoolClass.Students = null;
                teacherSchoolClass.Teachers = null;
            }
            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var teacher = await _context.Teachers.Include(s => s.SchoolClasses).FirstOrDefaultAsync(s => s.Id == id);
            if (teacher?.SchoolClasses == null) return Ok(teacher);
            foreach (var teacherSchoolClass in teacher.SchoolClasses)
            {
                teacherSchoolClass.Students = null;
                teacherSchoolClass.Teachers = null;
            }

            return Ok(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Shared.Model.Teacher teacher)
        {
            if (teacher.SchoolClasses != null)
                foreach (var teacherSchoolClass in teacher.SchoolClasses)
                {
                    _context.Entry(teacherSchoolClass).State = EntityState.Modified;
                }

            _context.Add(teacher);
            await _context.SaveChangesAsync();
            return Ok(teacher.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Shared.Model.Teacher teacher)
        {
            foreach (var teacherSchoolClass in teacher.SchoolClasses)
            {
                _context.Entry(teacherSchoolClass).State = EntityState.Modified;
            }
            _context.Entry(teacher).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = new Shared.Model.Teacher() { Id = id };
            _context.Remove(student);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
