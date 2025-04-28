using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models{
    [Table ("Categories")]
    public class Category{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength (100)]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }    
        [StringLength (100)]
        public string? Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateAt{ get; set; } = DateTime.UtcNow;
    }
}