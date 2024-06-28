#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;
namespace TaskManager.Models;
public class Event
{
  [Key]
  public int EventId {get;set;}
  [Required(ErrorMessage = "Task is required")]
  public DateTime EventTime {get;set;}
  public DateTime CreatedAt {get;set;} = DateTime.Now;
  public DateTime UpdatedAt {get;set;} = DateTime.Now;


  public int EventTaskId {get;set;}
  public ToDo EventTask {get;set;}
  public int TaskOwnerId {get;set;}
  public User TaskOwner {get;set;}

}