using System.ComponentModel.DataAnnotations;

namespace yado_backend.Models
{
    public class Country
    {
        [Key]
        [StringLength(2)]
        public required string Code { get; set; }

        [StringLength(50)]
        public string? Name { get; set; }

        public List<Population> Populations { get; set; } = new List<Population>();
    }
}

