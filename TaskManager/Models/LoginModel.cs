#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace TaskManager.Models;

public class Login
{
  [Required(ErrorMessage = "Email is required!")]
  [EmailAddress(ErrorMessage = "Invalid Email!")]
public string LoginEmail { get; set; }


  [Required(ErrorMessage = "Password is required!")]
  [DataType(DataType.Password)]
  [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
  public string LoginPassword { get; set;}
}