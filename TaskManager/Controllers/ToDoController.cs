using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ToDoManager.Controllers;
[SessionCheck] 
public class ToDoController : Controller
{
    private readonly ILogger<ToDoController> _logger;
    private readonly MyContext _context;

    public ToDoController(ILogger<ToDoController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }


  [HttpGet("ToDo/AllToDos")]
  public ViewResult AllToDos()
  {
    List<ToDo> AllToDos = _context.ToDos.Where(t => t.UserId == (int)HttpContext.Session.GetInt32("UserId")).ToList();
    return View("ToDos", AllToDos);
  }

  [HttpPost("ToDo/CreateToDo")]
  public IActionResult CreateToDo(ToDo newToDo)
  {
    if (ModelState.IsValid)
    {
      newToDo.UserId = (int)HttpContext.Session.GetInt32("UserId");
      _context.Add(newToDo);
      _context.SaveChanges();
      return RedirectToAction("AllToDos", "ToDo");
    }
    else
    {
      return View("ToDos");
    }
  }

  [HttpGet("ToDo/{toDoId}/EditToDo")]
  public ViewResult EditToDo(int ToDoId)
  {
    ToDo? ToDoToEdit = _context.ToDos.FirstOrDefault(t => t.TaskId == ToDoId);
    return View("EditToDo", ToDoToEdit);
  }

  [HttpPost("ToDo/{toDoId}/UpdateToDo")]
  public IActionResult UpdateToDo(int ToDoId, ToDo updateToDo)
  {
    ToDo? OldToDo = _context.ToDos.FirstOrDefault(t => t.TaskId == ToDoId);
    if(ModelState.IsValid)
    {
      OldToDo.TaskName = updateToDo.TaskName;
      OldToDo.Frequent = updateToDo.Frequent;
      OldToDo.UpdatedAt = DateTime.Now;
      _context.SaveChanges();
      return RedirectToAction("AllToDos", "ToDo");
    }
    else
    {
      return View("EditToDo");
    }
  }

  [HttpPost("ToDo/{toDoId}/Destroy")]
  public RedirectToActionResult DestroyToDo(int toDoId) 
  {
    return RedirectToAction("AllToDos", "Todo");
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