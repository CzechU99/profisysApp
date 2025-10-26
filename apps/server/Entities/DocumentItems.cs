using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace profisysApp.Entities
{
    public class DocumentItems
    {
        [Key]
        public int Id { get; set; }
        
        public int DocumentId { get; set; }

        public int Ordinal { get; set; }

        [Required]
        public string Product { get; set; } = null!;

        public int Quantity { get; set; }
        
        public double Price { get; set; }

        public int TaxRate { get; set; }

        [ForeignKey("DocumentId")]
        public Documents? Document { get; set; }
    }
}