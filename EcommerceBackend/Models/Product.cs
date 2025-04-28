using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models{
    [Table("Products")]
    public class Product{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [Column(TypeName ="varchar(100)")]
        public string Name { get; set; }    
        [StringLength(100)]
        [Column(TypeName ="varchar(255)")]
        public string Description { get; set; }  
        [Required]
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price {get;set;}
        [Required]
        [Column(TypeName ="int")]
        public int Instock {get;set;}
        [Column(TypeName ="nvarchar(255)")]
        public string? ImageUrl {get;set;}
        [Required]
        [ForeignKey("Category")]
        public int CategoryId {get;set;}
        //Category? Product.Category {get;set;}
        public Category? Category {get;set;}

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateAt {get;set;} = DateTime.UtcNow;  

    }
}