using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace profisysApp.Entities
{
    public class DocumentItems
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Documents")]
        public int DocumentId { get; set; }

        [Required]
        public int Ordinal { get; set; }

        [Required]
        public string Product { get; set; } = null!;

        [Required]
        public double Quantity { get; set; }
        
        [Required]
        public double TaxRate { get; set; }
    }
}