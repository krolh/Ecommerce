using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // Tạo mới sản phẩm
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] CreateProductRequest request)
        {
            try
            {
                var product = new Product
                {
                    Name = request.Name, // Sửa chỗ này!
                    Description = request.Description,
                    Price = request.Price,
                    Instock = request.Instock,
                    ImageUrl = request.ImageUrl,
                    CategoryId = request.CategoryId
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Phải có hàm GetProduct
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        // Class request phải public
        public class CreateProductRequest
        {
            public string Name { get; set; }
            public string? Description { get; set; }
            public int Price { get; set; }
            public int Instock { get; set; }
            public string ImageUrl { get; set; }
            public int CategoryId { get; set; }
        }
    }
}
