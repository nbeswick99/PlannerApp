#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace TaskManager.Models;
public class MyContext : DbContext 
{
    public MyContext(DbContextOptions options) : base(options) { }
    public DbSet<User> Users {get;set;}
    public DbSet<ToDo> ToDos {get;set;}
    public DbSet<Event> Events {get;set;}
}