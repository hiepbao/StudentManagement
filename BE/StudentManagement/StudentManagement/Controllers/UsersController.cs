using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.SignalR;
using StudentManagement.Hub;

namespace StudentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly StudentManagementContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public UsersController(StudentManagementContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
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

        [HttpGet("export-excel")]
        public async Task<IActionResult> ExportToExcel()
        {
            var users = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .ToListAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("UserData");
                worksheet.Cell(1, 1).Value = "STT";
                worksheet.Cell(1, 2).Value = "Họ tên";
                worksheet.Cell(1, 3).Value = "Giới tính";
                worksheet.Cell(1, 4).Value = "Ngày sinh";
                worksheet.Cell(1, 5).Value = "Địa chỉ";
                worksheet.Cell(1, 6).Value = "Số điện thoại";
                worksheet.Cell(1, 7).Value = "Email";
                worksheet.Cell(1, 8).Value = "Loại tài khoản";

                for (int i = 0; i < users.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = i + 1;
                    worksheet.Cell(i + 2, 2).Value = users[i].Name;
                    worksheet.Cell(i + 2, 3).Value = users[i].Gender;
                    worksheet.Cell(i + 2, 4).Value = users[i].Birthday;
                    worksheet.Cell(i + 2, 5).Value = users[i].Address;
                    worksheet.Cell(i + 2, 6).Value = users[i].Phone;
                    worksheet.Cell(i + 2, 7).Value = users[i].Mail;
                    worksheet.Cell(i + 2, 8).Value = string.Join(", ", users[i].UserRoles.Select(ur => ur.Role.RoleName));
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "UserData.xlsx");
                }
            }
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

        [HttpPost("import-excel")]
        public async Task<IActionResult> ImportFromExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { message = "Không có file." });
            }

            var errorMessages = new List<string>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed().Skip(1);

                    foreach (var row in rows)
                    {
                        var email = row.Cell(7).GetValue<string>();
                        var phoneNumber = row.Cell(6).GetValue<string>();
                        var roleName = row.Cell(8).GetValue<int>();

                        if (await _context.Users.AnyAsync(u => u.Mail == email))
                        {
                            errorMessages.Add($"Email {email} đã được đăng ký.");
                            continue;
                        }
                        if (await _context.Users.AnyAsync(u => u.Phone == phoneNumber))
                        {
                            errorMessages.Add($"Số điện thoại {phoneNumber} đã được đăng ký.");
                            continue;
                        }
                        var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == roleName);
                        if (role == null)
                        {
                            errorMessages.Add($"không tồn tại role {roleName}.");
                            continue;
                        }

                        var user = new User
                        {
                            Name = row.Cell(2).GetValue<string>(),
                            Gender = row.Cell(3).GetValue<string>(),
                            Birthday = row.Cell(4).GetValue<DateTime>(),
                            Address = row.Cell(5).GetValue<string>(),
                            Phone = phoneNumber,
                            Mail = email,
                            Password = BCrypt.Net.BCrypt.HashPassword("1") 
                        };
                        _context.Users.Add(user);
                        await _context.SaveChangesAsync();

                        user = await _context.Users.FirstOrDefaultAsync(u => u.Mail == email);
                        if (user == null)
                        {
                            errorMessages.Add($"Không thể tìm thấy người dùng {email} sau khi lưu.");
                            continue;
                        }
                        var userRole = new UserRole
                        {
                            UserId = user.UserId,
                            RoleId = roleName
                        };
                        _context.UserRoles.Add(userRole);
                    }
                }
            }

            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("UsersUpdated");

            if (errorMessages.Any())
            {
                return BadRequest(new { message = "Có lỗi trong quá trình nhập dữ liệu.", errors = errorMessages });
            }

            return Ok(new { message = "Thêm thành công." });
        }

        [HttpPost("addRole")]
        public async Task<IActionResult> Addrole(int userId, int roleId)
        {
            var user = await _context.Users.FindAsync(userId);
            var role = await _context.Roles.FindAsync(roleId);
            if (user == null)
            {
                throw new Exception($"User với UserId {userId} không tồn tại.");
            }

            if (role == null)
            {
                throw new Exception($"Role với RoleId {roleId} không tồn tại.");
            }

            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };

            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Thêm thành công." });
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
