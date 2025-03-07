using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using band_catalogue.Models;

namespace band_catalogue.Controllers;

// I am using MVCs so this is not a true REST API, but it does conform to the RESTful principles
// of using HTTP methods to perform CRUD operations on resources.
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
