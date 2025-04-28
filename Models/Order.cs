using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models{
    [Table ("Orders")]
    public class Order{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Users")]
        public int UserId {get;set;}
        [Required]
        [Column(TypeName ="decimal(18,2)")]
        public decimal TotalAmount {get;set;}
        [Required]
        [StringLength (100)]
        [Column(TypeName = "varchar(100)")]
        public string OrderStatus { get; set; }    
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateAt{ get; set; } = DateTime.UtcNow;
    }
}