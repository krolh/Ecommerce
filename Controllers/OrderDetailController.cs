using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers{
    [ApiController]
    [Route("api/OrderDetails")]

    public class OrderDetailsController: ControllerBase{
        private readonly AppDbContext _context;
        public OrderDetailsController(AppDbContext context){
            _context = context;
        }
        //Tao moi don hang chi tiet
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> CreateOrderDetail([FromBody] OrderDetailRequest request){
            try{
                var OrderDetail = new OrderDetail{
                    OrderId=request.OrderId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    Price = request.Price
                };
                _context.OrderDetails.Add(OrderDetail);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetOrderDetail), new {id = OrderDetail.Id},OrderDetail);
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
        {
            var OrderDetail = await _context.OrderDetails.FindAsync(id);
            if (OrderDetail == null)
            {
                return NotFound(); // Nếu không tìm thấy đơn hàng
            }
            return OrderDetail; // Trả về đơn hàng tìm được
        }
    public class OrderDetailRequest{
        public int OrderId { get; set;}
        public int ProductId { get; set;}
        public int Quantity {get;set;}
        public int Price {get;set;}
    }
    }
}