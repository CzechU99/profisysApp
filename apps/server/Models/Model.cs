using System.ComponentModel.DataAnnotations;
using profisysApp.Entities;

namespace profisysApp.Models
{
  public class LoginRequest
  {
    [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Hasło jest wymagane")]
    public string Password { get; set; } = string.Empty;
  }

  public class RegisterRequest
  {
    [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
    [MinLength(3, ErrorMessage = "Nazwa użytkownika musi mieć minimum 3 znaki")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Hasło jest wymagane")]
    [MinLength(6, ErrorMessage = "Hasło musi mieć minimum 6 znaków")]
    public string Password { get; set; } = string.Empty;
  }

  public class DocumentUpdateDto
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "Pole Typ jest wymagane.")]
    [MinLength(3, ErrorMessage = "Typ dokumentu musi mieć co najmniej 3 znaki.")]
    [MaxLength(50, ErrorMessage = "Typ dokumentu nie może przekraczać 50 znaków.")]
    public string Type { get; set; } = null!;

    [Required(ErrorMessage = "Data jest wymagana.")]
    public DateOnly Date { get; set; }

    [Required(ErrorMessage = "Imię jest wymagane.")]
    [MinLength(2, ErrorMessage = "Imię musi mieć co najmniej 2 znaki.")]
    [MaxLength(50, ErrorMessage = "Imię nie może przekraczać 50 znaków.")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Nazwisko jest wymagane.")]
    [MinLength(2, ErrorMessage = "Nazwisko musi mieć co najmniej 2 znaki.")]
    [MaxLength(50, ErrorMessage = "Nazwisko nie może przekraczać 50 znaków.")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Miasto jest wymagane.")]
    [MinLength(2, ErrorMessage = "Nazwa miasta musi mieć co najmniej 2 znaki.")]
    [MaxLength(100, ErrorMessage = "Nazwa miasta nie może przekraczać 100 znaków.")]
    public string City { get; set; } = null!;
  }

  public class ItemUpdateDto
  {
    public int Id { get; set; }

    public int DocumentId { get; set; }

    public int Ordinal { get; set; }

    [Required(ErrorMessage = "Pole Produkt jest wymagane.")]
    [MinLength(3, ErrorMessage = "Nazwa produktu musi mieć co najmniej 3 znaki.")]
    public string Product { get; set; } = null!;

    [Range(1, 100, ErrorMessage = "Ilość musi mieścić się w przedziale od 1 do 100.")]
    public int Quantity { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Cena musi być większa niż 0.")]
    public double Price { get; set; }

    [Range(0, 100, ErrorMessage = "Stawka podatku musi być pomiędzy 0 a 100%.")]
    public int TaxRate { get; set; }

  }
}