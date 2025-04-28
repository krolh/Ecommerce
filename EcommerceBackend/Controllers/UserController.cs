
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
//using Data;

namespace Controllers{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UserController(AppDbContext context){
            _context = context;
        }
        //Tao moi user
        [HttpPost]
        public async Task<ActionResult<Users>> CreateUser([FromBody] CreateUserRequest request)
        {
            //Kiem tra xem username da ton tai chua
            if(await _context.Users.AnyAsync(u => u.Username == request.Username))
                return BadRequest("Username da ton tai.");
            var user = new Users{
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Fullname = request.Fullname,
                Role = "nhanvien",
                Phone = request.Phone,
                IsActive = true
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new{id = user.Id}, user);
        }
        //lay nguoi dung
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>>GetUser(int id){
            var user = await _context.Users.FindAsync(id);
            if(user == null)
                return NotFound();
            return user;
        }
    }
    public class CreateUserRequest{
        public string Username{get;set;}
        public string Password{get;set;}
        public string Fullname{get;set;}
        public string Phone{get;set;}
    }
}