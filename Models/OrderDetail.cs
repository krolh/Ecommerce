using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models{
    [Table ("OrderDetails")]
    public class OrderDetail{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Order")]
        public int OrderId {get;set;}
        [Required]
        [ForeignKey("Product")]
        public int ProductId {get;set;}
        [Required]
        public int Quantity {get;set;}
        [Required]
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price {get;set;}
        
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public DateTime CreateAt{ get; set; } = DateTime.UtcNow;
    }
}