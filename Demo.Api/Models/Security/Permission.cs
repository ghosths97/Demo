using System.ComponentModel.DataAnnotations;

namespace Demo.Api.Models.Security
{
    public class Permission
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
