using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
//using Models;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config; 
    public AuthController(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }
    //Api Đăng nhập
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _context.Users
        .FirstOrDefaultAsync(
            u => u.Username == request.Username);
        String hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            return Unauthorized();
        }  
        
        //Tạp token cho phiên đăng nhập
        var token = GenerateJwtToken(user);
        //Trả về thông tin người dùng và token    
        return Ok(new{Token = token, User = user});
    }
    //Phương thức tạo token
    private String GenerateJwtToken(Users user){
        var jwtKey = _config["Jwt:Key"] ?? throw new InvalidOperationException("JWT key chưa được cau hinh");
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]{
            new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
            new Claim(ClaimTypes.Role,user.Role),
        };
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:ExpiryInMinutes"])),
            signingCredentials:credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public class LoginRequest
{
    public String Username { get; set; }
    public String Password { get; set; }
}