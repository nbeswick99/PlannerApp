using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using Microsoft.AspNetCore.Identity;

namespace TaskManager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View("Index");
    }

    [HttpPost("Home/CreateUser")]
    public IActionResult CreateUser(User newUser)
    {
        if(ModelState.IsValid)
        {
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
            _context.Users.Add(newUser);
            _context.SaveChanges(); 
            HttpContext.Session.SetInt32("UserId", newUser.UserId);
            return RedirectToAction("Dashboard", "Home");
        }
        else
        {
            return View("Index");
        }
    }

    [HttpPost("Home/Login")]
    public IActionResult Login(Login userSubmission)
    {
        if(ModelState.IsValid)
        {
        User? DBLogin = _context.Users.FirstOrDefault(e => e.Email == userSubmission.LoginEmail);
        if (DBLogin == null)
        {
            ModelState.AddModelError("LoginEmail", "Email does not exist");
            return View("Index");
        }
        PasswordHasher<Login> Hasher = new PasswordHasher<Login>();
        var result = Hasher.VerifyHashedPassword(userSubmission, DBLogin.Password, userSubmission.LoginPassword);
        
        if(result == 0)
        {
            ModelState.AddModelError("LoginPassword", "Invalid Email/Password");
            return View("Index");
        }
        
        HttpContext.Session.SetInt32("UserId", DBLogin.UserId);
        return RedirectToAction("Dashboard", "Home");
        }
        else
        {
            return View("Index");
        }
    }

    [HttpGet("Home/Dashboard")]
    public ViewResult Dashboard() 
    {
    return View();
    }

    [HttpPost("Home/Logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
