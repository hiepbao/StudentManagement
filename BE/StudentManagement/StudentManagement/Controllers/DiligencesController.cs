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
    public class DiligencesController : ControllerBase
    {
        private readonly StudentManagementContext _context;

        public DiligencesController(StudentManagementContext context)
        {
            _context = context;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<Diligence>>> GetDiligences()
        {
            return await _context.Diligences.ToListAsync();
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<Diligence>> GetDiligence(int id)
        {
            var diligence = await _context.Diligences.FindAsync(id);

            if (diligence == null)
            {
                return NotFound(new { message = "Không có thông tin này" });
            }

            return diligence;
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> PutDiligence(int id, Diligence diligence)
        {
            if (id != diligence.DiligenceId)
            {
                return BadRequest(new { message = "Không có thông tin này" });
            }

            _context.Entry(diligence).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiligenceExists(id))
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

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateDiligence(int id,  string ExcusedAbsence, string unExcusedAbsence)
        {
            var diligence = await _context.Diligences.FindAsync(id);
            if (diligence == null || id != diligence.DiligenceId)
            {
                return BadRequest(new { message = "Invalid diligence data." });
            }

            var existingDiligence = await _context.Diligences.FindAsync(id);
            if (existingDiligence == null)
            {
                return NotFound(new { message = "Diligence not found." });
            }

            var semester = await _context.Semesters.FindAsync(diligence.SemesterId);
            if (semester == null)
            {
                return NotFound(new { message = "Semester not found." });
            }

            if (DateTime.Now < semester.StartSemester || DateTime.Now > semester.EndSemester)
            {
                return BadRequest(new { message = "Invalid semester." });
            }

            existingDiligence.ExcusedAbsence = ExcusedAbsence;
            existingDiligence.UnexcusedAbsence = unExcusedAbsence;

            int excusedAbsence = 0;
            int unexcusedAbsence = 0;

            if (!string.IsNullOrEmpty(diligence.ExcusedAbsence))
            {
                excusedAbsence = int.TryParse(diligence.ExcusedAbsence, out int parsedExcused) ? parsedExcused : 0;
            }

            if (!string.IsNullOrEmpty(diligence.UnexcusedAbsence))
            {
                unexcusedAbsence = int.TryParse(diligence.UnexcusedAbsence, out int parsedUnexcused) ? parsedUnexcused : 0;
            }

            if (excusedAbsence > 10 || unexcusedAbsence > 5)
            {
                existingDiligence.AttendanceRating = "Yếu";
            }
            else if ((excusedAbsence >= 6 && excusedAbsence <= 10))
            {
                existingDiligence.AttendanceRating = "Khá";
            }
            else if ((unexcusedAbsence >= 2 && unexcusedAbsence <= 5))
            {
                existingDiligence.AttendanceRating = "Khá";
            }
            else if ((excusedAbsence >= 1 && excusedAbsence <= 5))
            {
                existingDiligence.AttendanceRating = "Tốt";
            }
            else if (unexcusedAbsence <= 1)
            {
                existingDiligence.AttendanceRating = "Tốt";
            }
            else
            {
                existingDiligence.AttendanceRating = "Không Xác định";
            }

            _context.Diligences.Update(existingDiligence);
            await _context.SaveChangesAsync();

            return Ok(existingDiligence);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Diligence>> PostDiligence(Diligence diligence)
        {
            if (diligence == null)
            {
                return BadRequest(new { message = "Không có thông tin này" });
            }

            var semester = await _context.Semesters.FindAsync(diligence.SemesterId);
            if (semester == null)
            {
                return NotFound(new { message = "Không tìm thấy học kỳ" });
            }

            if (DateTime.Now < semester.StartSemester || DateTime.Now > semester.EndSemester)
            {
                return BadRequest(new { message = "Học kỳ không đúng" });
            }

            _context.Diligences.Add(diligence);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiligence", new { id = diligence.DiligenceId }, diligence);
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteDiligence(int id)
        {
            var diligence = await _context.Diligences.FindAsync(id);
            if (diligence == null)
            {
                return NotFound(new { message = "Không có thông tin này" });
            }

            _context.Diligences.Remove(diligence);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Xóa thành công" });
        }

        private bool DiligenceExists(int id)
        {
            return _context.Diligences.Any(e => e.DiligenceId == id);
        }
    }
}
