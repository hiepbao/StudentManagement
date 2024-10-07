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
    public class StudyPointsController : ControllerBase
    {
        private readonly StudentManagementContext _context;

        public StudyPointsController(StudentManagementContext context)
        {
            _context = context;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<StudyPoint>>> GetStudyPoints()
        {
            return await _context.StudyPoints.ToListAsync();
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<StudyPoint>> GetStudyPoint(int id)
        {
            var studyPoint = await _context.StudyPoints.FindAsync(id);

            if (studyPoint == null)
            {
                return NotFound(new { message = "Không có điểm này" });
            }

            return studyPoint;
        }

        [HttpGet("GetByParent/{studentId}")]
        public async Task<ActionResult<IEnumerable<StudyPoint>>> GetStudyPointsByParent(int studentId)
        {
            var studyPoints = await _context.StudyPoints.Include(sp => sp.Student)
                .Include(sp => sp.Class)
                .Include(sp => sp.Teacher)
                .Include(sp => sp.Subject)
                .Include(sp => sp.Semester)
                .Where(sp => sp.StudentId == studentId).ToListAsync();

            if (studyPoints == null || !studyPoints.Any())
            {
                return NotFound(new { message = "Không tìm thấy điểm cho học sinh này" });
            }

            return Ok(studyPoints);
        }

        [HttpGet("GetByTeacher/{teacherId}")]
        public async Task<ActionResult<IEnumerable<StudyPoint>>> GetStudyPointsByTeacher(int teacherId)
        {
            var studyPoints = await _context.StudyPoints.Include(sp => sp.Student)
                .Include(sp => sp.Class)
                .Include(sp => sp.Teacher)
                .Include(sp => sp.Subject)
                .Include(sp => sp.Semester)
                .Where(sp => sp.TeacherId == teacherId).ToListAsync();

            if (studyPoints == null || !studyPoints.Any())
            {
                return NotFound(new { message = "Không tìm thấy điểm cho giáo viên này" });
            }

            return Ok(studyPoints);
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> PutStudyPoint(int id, StudyPoint studyPoint)
        {
            if (id != studyPoint.PointId)
            {
                return BadRequest(new { message = "Không có điểm này" });
            }

            _context.Entry(studyPoint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudyPointExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Cập nhập thành công" });
        }

        [HttpPut("EditPoint/{id}")]
        public async Task<IActionResult> PutStudyPoint(int id, decimal gradeFactor1, decimal gradeFactor2, decimal gradeFactor3)
        {
            var studyPoint = await _context.StudyPoints.FindAsync(id);

            if (studyPoint == null)
            {
                return NotFound("Không tìm thấy điểm học tập");
            }

            // Cập nhật điểm và ngày tháng hiện tại
            studyPoint.GradeFactor1 = gradeFactor1;
            studyPoint.GradeFactor2 = gradeFactor2;
            studyPoint.GradeFactor3 = gradeFactor3;
            studyPoint.AveragePoint = (gradeFactor1 + 2* gradeFactor2 + 3* gradeFactor3) / 6;
            studyPoint.GradeEntryDate = DateTime.Now;

            _context.Entry(studyPoint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.StudyPoints.Any(e => e.PointId == id))
                {
                    return NotFound("Không tìm thấy điểm học tập");
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Cập nhập thành công" });
        }

        [HttpPost("Create")]
        public async Task<ActionResult<StudyPoint>> PostStudyPoint(StudyPoint studyPoint)
        {
            _context.StudyPoints.Add(studyPoint);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudyPoint", new { id = studyPoint.PointId }, studyPoint);
        }

        [HttpPost("AddPoint")]
        public async Task<IActionResult> AddStudyPoint(int tId, int sId, int cId, int semId, decimal gf1, decimal gf2, decimal gf3)
        {
            var teacher = await _context.Teachers.FindAsync(tId);
            if (teacher == null)
            {
                return NotFound(new { message = "Không tìm thấy giáo viên" });
            }

            var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.SubjectId == teacher.SubjectId);


            var semester = await _context.Semesters.FindAsync(semId);
            if (semester == null)
            {
                return NotFound(new { message = "Không tìm thấy học kỳ" });
            }

            if (DateTime.Now < semester.StartSemester || DateTime.Now > semester.EndSemester)
            {
                return BadRequest(new { message = "Học kỳ không đúng" });
            }

            var studyPoint = new StudyPoint
            {
                StudentId = sId,
                ClassId = cId,
                TeacherId = tId,
                SubjectId = subject.SubjectId,
                SemesterId = semId,
                GradeFactor1 = gf1,
                GradeFactor2 = gf2,
                GradeFactor3 = gf3,
                AveragePoint = (gf1 +  2 * gf2 +  3 *gf3) / 6,
                GradeEntryDate = DateTime.Now
            };

            _context.StudyPoints.Add(studyPoint);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Thêm điểm học tập thành công" });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteStudyPoint(int id)
        {
            var studyPoint = await _context.StudyPoints.FindAsync(id);
            if (studyPoint == null)
            {
                return NotFound(new { message = "Không có điểm này" });
            }

            _context.StudyPoints.Remove(studyPoint);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Xóa thành công" });
        }

        private bool StudyPointExists(int id)
        {
            return _context.StudyPoints.Any(e => e.PointId == id);
        }
    }
}
