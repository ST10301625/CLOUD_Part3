using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CloudPart3.Models
{
    [Table("Style")]
    public class Style
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string StyleName { get; set; }
        public List<Product> Products { get; set; }
    }
}
