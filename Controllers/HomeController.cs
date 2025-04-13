using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ITMOAspNetMvcApp.Models;
using ITMOAspNetMvcApp.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ITMOAspNetMvcApp.Controllers{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        // Асинхронный метод для получения данных из представления AttendanceRecords
        public async Task<IActionResult> Index()
        {
            var attendanceRecords = await _context.AttendanceRecords.ToListAsync();
            return View(attendanceRecords);
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
}