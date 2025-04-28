using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models{
    [Table("Reviews")]
    public class Review{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{get;set;}

        [Required]
        [ForeignKey("Users")]
        public int UserId {get;set;}

        [Required]
        [ForeignKey("Product")]
        public int ProductId {get;set;}

        [Required]
        public int Rating {get;set;}

        [Column(TypeName = "nvarchar(100)")]
        public string Comment {get;set;}

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateAt {get;set;} = DateTime.UtcNow;

    }
}