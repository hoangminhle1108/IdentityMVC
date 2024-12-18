using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace IdentityCus_Temp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(255)]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 255, ErrorMessage = "Display Order must be between 0-255")]
        public int DisplayOrder { get; set; }
    }
}
