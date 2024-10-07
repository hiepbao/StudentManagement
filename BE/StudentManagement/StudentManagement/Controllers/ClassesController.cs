using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly StudentManagementContext _context;

        public ClassesController(StudentManagementContext context)
        {
            _context = context;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
        {
            return await _context.Classes.Include(c => c.Grade).ToListAsync();
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetClass(int id)
        {
            var @class = await _context.Classes
                .Include(c => c.Grade)
                .Include(c => c.ClassDetails)
                    .ThenInclude(cd => cd.HomeroomTeacher)
                .Include(c => c.ClassDetails)
                    .ThenInclude(cd => cd.Teacher)
                .Include(c => c.Students)
                .Include(c => c.StudyPoints)
                .Include(c => c.Diligences)
                .FirstOrDefaultAsync(c => c.ClassId == id);

            if (@class == null)
            {
                return NotFound(new { message = "Không tìm thấy lớp với ID đã cho." });
            }

            return Ok(@class);
        }

        [HttpPut("EditFull/{id}")]
        public async Task<IActionResult> PutClass(int id, Class @class)
        {
            if (id != @class.ClassId)
            {
                return NotFound(new { message = "Không có lớp này" });
            }

            _context.Entry(@class).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(id))
                {
                    return NotFound(new { message = "Cập nhập không thành công" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Cập nhập thành công" });
        }

        [HttpPost("Creat")]
        public async Task<ActionResult<Class>> PostClass(Class @class)
        {
            _context.Classes.Add(@class);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClass", new { id = @class.ClassId }, @class);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var @class = await _context.Classes.FindAsync(id);
            if (@class == null)
            {
                return NotFound(new { message = "Không có lớp này" });
            }

            _context.Classes.Remove(@class);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Xóa thành công" });
        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.ClassId == id);
        }
    }
}
