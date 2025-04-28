using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("CartItems")]
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; } // Đã sửa bigint -> long

        [Required]
        [ForeignKey("Users")]
        public int UserId { get; set; }
        [Required]
        [ForeignKey("Products")]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        // Không cần [DatabaseGenerated] cho DateTime
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}
