using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers{
    [ApiController]
    [Route("api/Reviews")]

    public class ReviewsController: ControllerBase{
        private readonly AppDbContext _context;
        public ReviewsController(AppDbContext context){
            _context = context;
        }
        //Tao moi review
        [HttpPost]
        public async Task<ActionResult<Review>> CreateReview([FromBody] ReviewRequest request){
            try{
                var Review = new Review{
                    UserId=request.UserId,
                    ProductId = request.ProductId,
                    Rating = request.Rating,
                    Comment = request.Comment
                };
                _context.Reviews.Add(Review);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetReview), new {id = Review.Id},Review);
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var Review = await _context.Reviews.FindAsync(id);
            if (Review == null)
            {
                return NotFound(); // Nếu không tìm review
            }
            return Review; // Trả về review tìm được
        }
    public class ReviewRequest{
        public int UserId { get; set;}
        public int ProductId { get; set;}
        public int Rating {get;set;}
        public string Comment {get;set;}
    }
    }
}