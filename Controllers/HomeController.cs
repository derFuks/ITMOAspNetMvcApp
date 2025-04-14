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
        public async Task<IActionResult> Index(int? selectedStudentId)
        {
            var students = await _context.Students.ToListAsync();

            Student selectedStudent = null;
            if (selectedStudentId != null)
            {
                selectedStudent = await _context.Students
                    .FirstOrDefaultAsync(s => s.StudentId == selectedStudentId);
            }

            // 5 лучших
            var best = students
                .OrderByDescending(s => s.TotalAttendanceScore)
                .Take(5)
                .ToList();

            // топ худших
            var worst = students
                .OrderBy(s => s.TotalAttendanceScore)
                .Take(5)
                .ToList();

            // Проверка на дубликаты в значениях
            bool bestTied = students
                .Count(s => s.TotalAttendanceScore == best.Last().TotalAttendanceScore) > 5;

            bool worstTied = students
                .Count(s => s.TotalAttendanceScore == worst.Last().TotalAttendanceScore) > 5;

            ViewBag.Students = students;
            ViewBag.SelectedStudent = selectedStudent;
            ViewBag.Best = best;
            ViewBag.Worst = worst;
            ViewBag.BestTied = bestTied;
            ViewBag.WorstTied = worstTied;

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