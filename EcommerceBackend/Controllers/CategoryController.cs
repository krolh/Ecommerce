
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers{
    [ApiController]
    [Route("api/categories")]

    public class CategoryController: ControllerBase{
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context){
            _context = context;
        }
        //Tao moi danh muc
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] CategoryRequest request){
            try{
                var Category = new Category{
                    Name=request.Name,
                    Description = request.Description
                };
                _context.Categories.Add(Category);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCategory), new {id = Category.Id},Category);
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound(); // Nếu không tìm thấy thì trả về NotFound
            }
            return category; // Trả về category tìm được
        }
    public class CategoryRequest{
        public string Name { get; set;}
        public string? Description { get; set;}
    }
    }
}