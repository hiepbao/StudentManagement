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
    public class ClassDetailsController : ControllerBase
    {
        private readonly StudentManagementContext _context;

        public ClassDetailsController(StudentManagementContext context)
        {
            _context = context;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<ClassDetail>>> GetClassDetails()
        {
            return await _context.ClassDetails.ToListAsync();
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<ClassDetail>> GetClassDetail(int id)
        {
            var classDetail = await _context.ClassDetails.FindAsync(id);

            if (classDetail == null)
            {
                return NotFound();
            }

            return classDetail;
        }

        [HttpPut("Editfull/{id}")]
        public async Task<IActionResult> PutClassDetail(int id, ClassDetail classDetail)
        {
            if (id != classDetail.ClassDetailId)
            {
                return BadRequest();
            }

            _context.Entry(classDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost("Creat")]
        public async Task<ActionResult<ClassDetail>> PostClassDetail(ClassDetail classDetail)
        {
            var teacher = await _context.Teachers
                .Include(t => t.Subject)
                .FirstOrDefaultAsync(t => t.TeacherId == classDetail.TeacherId);

            if (teacher != null && teacher.Subject != null)
            {
                classDetail.Subject = teacher.Subject.SubjectName; // Assuming Subject has a property SubjectName
            }

            _context.ClassDetails.Add(classDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClassDetail", new { id = classDetail.ClassDetailId }, classDetail);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteClassDetail(int id)
        {
            var classDetail = await _context.ClassDetails.FindAsync(id);
            if (classDetail == null)
            {
                return NotFound();
            }

            _context.ClassDetails.Remove(classDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassDetailExists(int id)
        {
            return _context.ClassDetails.Any(e => e.ClassDetailId == id);
        }
    }
}
