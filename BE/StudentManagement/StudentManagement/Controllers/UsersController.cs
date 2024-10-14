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
    public class UsersController : ControllerBase
    {
        private readonly StudentManagementContext _context;

        public UsersController(StudentManagementContext context)
        {
            _context = context;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .ToListAsync();

            return users;
        }

        // GET: api/Users/Search
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<User>>> SearchUsers([FromQuery] string? searchTerm)
        {
            var query = _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(u =>
                    u.Name.Contains(searchTerm) ||
                    u.Phone.Contains(searchTerm) ||
                    u.Mail.Contains(searchTerm));
            }

            var users = await query.ToListAsync();

            if (users == null || users.Count == 0)
            {
                return NotFound(new { message = "Không tìm thấy người dùng phù hợp" });
            }

            return users;
        }


        [HttpPut("Editfull/{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            var userToUpdate = await _context.Users.FindAsync(id);

            if (userToUpdate == null)
            {
                return NotFound(new { message = "Không có User này" });
            }

            userToUpdate.Name = user.Name;
            userToUpdate.Gender = user.Gender;
            userToUpdate.Address = user.Address;
            userToUpdate.Phone = user.Phone;
            userToUpdate.Mail = user.Mail;
            userToUpdate.Password = user.Password;
            userToUpdate.Birthday = user.Birthday;

            _context.Entry(userToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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


        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound(new { message = "Không có User này" });
            }

            if (!string.IsNullOrEmpty(user.Name))
            {
                existingUser.Name = user.Name;
                _context.Entry(existingUser).Property(x => x.Name).IsModified = true;
            }

            if (!string.IsNullOrEmpty(user.Mail))
            {
                existingUser.Mail = user.Mail;
                _context.Entry(existingUser).Property(x => x.Mail).IsModified = true;
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound(new { message = "Cập nhập không thành công" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Cập nhập thành công" }); ;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (await _context.Users.AnyAsync(u => u.Phone == user.Phone))
            {
                return BadRequest(new { message = "Số điện thoại đã được đăng ký." });
            }

            if (await _context.Users.AnyAsync(u => u.Mail == user.Mail))
            {
                return BadRequest(new { message = "Email đã được đăng ký." });
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Đăng ký thành công." });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "Không có User này" });
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Xóa thành công" });
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
