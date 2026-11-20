using Microsoft.AspNetCore.Mvc;

namespace MiBanco.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}