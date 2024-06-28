#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
namespace TaskManager.Models;
public class BigView
{
  public List<Event> Events {get;set;} 
  public List<ToDo> ToDos{get;set;}
}