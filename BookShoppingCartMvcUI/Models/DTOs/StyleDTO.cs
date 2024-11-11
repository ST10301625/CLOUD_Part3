using System.ComponentModel.DataAnnotations;

namespace CloudPart3.Models.DTOs
{
    public class StyleDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string StyleName { get; set; }
    }
}
