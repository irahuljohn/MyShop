using System.ComponentModel.DataAnnotations;

namespace MyShop.Models.Entities
{
    public class BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; } = null;
        public string? CreatedBy { get; set; } = string.Empty;
        public string? UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedDate { get; set; } = null;
    }
}
