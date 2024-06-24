using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
  public class UserViewModel
  {
    public long Id { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Surname { get; set; }

    [Required]
    public int Age { get; set; }
  }
}