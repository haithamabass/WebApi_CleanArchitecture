using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Brand_Id { get; set; }


        [Required]
        [MaxLength(100)]
        public string Brand_Name { get; set; }
    }
}