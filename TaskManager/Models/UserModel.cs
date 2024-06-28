#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Validations;
namespace TaskManager.Models;
public class User
{
  [Key]

  public int UserId {get;set;}

  [Required(ErrorMessage = "Name is required!")]
  [MinLength(2, ErrorMessage = "Name must be at least 2 characters!")]
  public string Name { get; set; }

  [Required(ErrorMessage = "Email is required!")]
  [EmailAddress(ErrorMessage = "Invalid Email!")]
  [UniqueEmail]
  public string Email { get; set; }

  [Required(ErrorMessage = "Password is required!")]
  [DataType(DataType.Password)]
  [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
  public string Password { get; set; }

  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public DateTime UpdatedAt { get; set; } = DateTime.Now;

  //Verify password
  [Required(ErrorMessage = "Confirm Password is required!")]
  [NotMapped]
  [DataType(DataType.Password)]
  [Compare("Password", ErrorMessage = "Passwords did not match")]
  public string Confirm { get; set; }

  //Navigation Properties
  List<ToDo> UserToDos {get;set;} = new();
  List<Event> UserEvents {get;set;} = new();

}