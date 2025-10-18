using System.ComponentModel.DataAnnotations;

namespace profisysApp.Entities
{
    public class Documents
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; } = null!;

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;
        
        [Required]
        public string City { get; set; } = null!;
    }
}