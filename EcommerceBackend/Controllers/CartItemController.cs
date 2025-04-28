using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("api/CartItems")]
    public class CartItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartItemsController(AppDbContext context)
        {
            _context = context;
        }

        // Tạo mới giỏ hàng
        [HttpPost]
        public async Task<ActionResult<CartItem>> CreateCartItem([FromBody] CartItemRequest request)
        {
            try
            {
                var cartItem = new CartItem
                {
                    UserId = request.UserId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                };

                _context.CartItems.Add(cartItem);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCartItem), new { id = cartItem.Id }, cartItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Lấy giỏ hàng theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<CartItem>> GetCartItem(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound(); // Nếu không tìm thấy giỏ hàng
            }
            return cartItem; // Trả về giỏ hàng tìm được
        }

        public class CartItemRequest
        {
            public int UserId { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
