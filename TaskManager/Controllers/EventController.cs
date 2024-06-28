using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Controllers;
[SessionCheck] 
public class EventController : Controller
{
    private readonly ILogger<EventController> _logger;
    private readonly MyContext _context;

    public EventController(ILogger<EventController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost("Event/{toDoId}/CreateEvent")]
    public RedirectToActionResult CreateEvent( int toDoId)
    {
        Event newEvent = new();
        newEvent.TaskOwnerId =(int)HttpContext.Session.GetInt32("UserId");
        newEvent.EventTaskId = toDoId;
        newEvent.EventTime = DateTime.Now;
        _context.Events.Add(newEvent);
        _context.SaveChanges();
        return RedirectToAction("Dashboard", "Home");
    }

    [HttpPost("Event/{eventId}/DestroyEvent")]
    public RedirectToActionResult DestroyEvent(int eventId)
    {
        Event DeleteEvent = _context.Events.FirstOrDefault(id => id.EventId == eventId && id.TaskOwnerId == (int)HttpContext.Session.GetInt32("UserId"));
        _context.Events.Remove(DeleteEvent);
        _context.SaveChanges();
        return RedirectToAction("Dashboard", "Home");
    }

    [HttpPost("Event/{eventId}/CompleteEvent")]
    public RedirectToActionResult CompleteEvent(int eventId)
    {
        Event CompleteEvent = _context.Events.Include(Event => Event.EventTask)
                                            .SingleOrDefault(id => id.EventId == eventId);

        if(CompleteEvent.EventTask.Frequent)
        {
            _context.Events.Remove(CompleteEvent);
            _context.SaveChanges();
            return RedirectToAction("Dashboard", "Home");
        }
        else
        {
            ToDo DeleteToDo = _context.ToDos.SingleOrDefault(id => id.TaskId == CompleteEvent.EventTask.TaskId);
            _context.Events.Remove(CompleteEvent);
            _context.Remove(DeleteToDo);
            _context.SaveChanges();
            return RedirectToAction("Dashboard", "Home");
        }
    }
}

public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        int? userId = context.HttpContext.Session.GetInt32("UserId");
        if(userId ==null)
        {
            context.Result = new RedirectToActionResult("Index", "home", null);
        }
    }
}