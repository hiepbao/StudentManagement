using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;

namespace StudentManagement.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly StudentManagementContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(StudentManagementContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User userLoginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Phone == userLoginDto.Phone);

            if (user == null || !BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.Password))
            {
                return Unauthorized(new { message = "Số điện thoại hoặc mật khẩu không đúng." });
            }

            var token = GenerateJwtToken(user);
            return Ok(new { message = "Đăng nhập thành công.", token });
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Phone == changePasswordDto.Phone);

            if (user == null)
            {
                return NotFound(new { message = "Người dùng không tồn tại." });
            }

            if (!BCrypt.Net.BCrypt.Verify(changePasswordDto.OldPassword, user.Password))
            {
                return BadRequest(new { message = "Mật khẩu cũ không đúng." });
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Đổi mật khẩu thành công." });
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var key = _configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key", "JWT key cannot be null");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Phone ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Name", user.Name ?? ""),
                new Claim("Gender", user.Gender ?? ""),
                new Claim("Address", user.Address ?? ""),
                new Claim("Mail", user.Mail ?? ""),
                new Claim("Birthday", user.Birthday?.ToString("o") ?? "")
            };

            var userRoles = await _context.UserRoles.Include(ur => ur.Role).Where(ur => ur.UserId == user.UserId).ToListAsync();
            foreach (var userRole in userRoles)
            {
                if (userRole.Role != null)
                {
                    claims.Add(new Claim("Role", userRole.Role.RoleName ?? ""));
                    var functions = await _context.Funtions.Where(f => f.RoleId == userRole.RoleId).ToListAsync();
                    foreach (var function in functions)
                    {
                        claims.Add(new Claim("Function", function.FunctionName ?? ""));
                        claims.Add(new Claim("Route", function.Route ?? ""));
                    }
                }
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
    public class ChangePasswordDto
    {
        public string Phone { get; set; } = string.Empty;
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
