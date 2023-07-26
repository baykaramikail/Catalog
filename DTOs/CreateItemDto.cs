using System.ComponentModel.DataAnnotations;

namespace Catalog.DTOs
{
    public record CreateItemDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1,100)]
        public decimal Price { get; set; }
    }
}
