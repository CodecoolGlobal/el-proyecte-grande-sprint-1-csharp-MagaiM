using System.Diagnostics;
using ElProyecteGrandeSprint1.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElProyecteGrandeSprint1.Controllers
{
    public class DealsController : Controller
    {
        private readonly ILogger<DealsController> _logger;

        public DealsController(ILogger<DealsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
