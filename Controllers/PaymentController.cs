using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers{
    [ApiController]
    [Route("api/Payments")]

    public class PaymentsController: ControllerBase{
        private readonly AppDbContext _context;
        public PaymentsController(AppDbContext context){
            _context = context;
        }
        //Tao moi don
        [HttpPost]
        public async Task<ActionResult<Payment>> CreatePayment([FromBody] PaymentRequest request){
            try{
                var Payment = new Payment{
                    OrderId=request.OrderId,
                    PaymentMethod = request.PaymentMethod,
                    PaymentStatus = request.PaymentStatus,
                    TransactionId = request.TransactionId,
                    PaidAmount = request.PaidAmount
                
                };
                _context.Payments.Add(Payment);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPayment), new {id = Payment.Id},Payment);
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            var Payment = await _context.Payments.FindAsync(id);
            if (Payment == null)
            {
                return NotFound(); // Nếu không tìm thấy đơn hàng
            }
            return Payment; // Trả về đơn hàng tìm được
        }
    public class PaymentRequest{
        public int OrderId { get; set;}
        public string PaymentMethod { get; set;}
        public string PaymentStatus {get;set;}
        public string TransactionId {get;set;}
        public decimal PaidAmount {get;set;}
    }
    }
}