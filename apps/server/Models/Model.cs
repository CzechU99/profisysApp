using System.ComponentModel.DataAnnotations;

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
}