using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp_UnderTheHood.Entities.Helper;
using WebApp_UnderTheHood.Models;

namespace WebApp_UnderTheHood.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            Console.WriteLine($"Cookie: {HttpContext.User.Identity?.IsAuthenticated}");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Constants.HRDeptPolicy)]
        [HttpGet]
        [Route("hr-home-page")]
        public IActionResult HRHomePage()
        {
            return View();
        }


        /// <summary>
        /// We'll display dashboard data to co-ordinators only let's say who has joined in / before 2023
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("dashboard")]
        [Authorize(Constants.CoOrdinatorsPolicy)]
        public IActionResult Dashboard()
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