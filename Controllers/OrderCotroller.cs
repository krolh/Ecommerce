using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers{
    [ApiController]
    [Route("api/Orders")]

    public class OrderController: ControllerBase{
        private readonly AppDbContext _context;
        public OrderController(AppDbContext context){
            _context = context;
        }
        //Tao moi don
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] OrderRequest request){
            try{
                var Order = new Order{
                    UserId=request.UserId,
                    TotalAmount = request.TotalAmount,
                    OrderStatus = request.OrderStatus
                };
                _context.Orders.Add(Order);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetOrder), new {id = Order.Id},Order);
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var Order = await _context.Orders.FindAsync(id);
            if (Order == null)
            {
                return NotFound(); // Nếu không tìm thấy đơn hàng
            }
            return Order; // Trả về đơn hàng tìm được
        }
    public class OrderRequest{
        public int UserId { get; set;}
        public decimal TotalAmount { get; set;}
        public string OrderStatus {get;set;}
    }
    }
}