using System.Linq;
using System.Threading.Tasks;
using ITMOAspNetMvcApp.Data;
using ITMOAspNetMvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITMOAspNetMvcApp.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Attendance
        public async Task<IActionResult> Index()
        {
            // Подключаем связанные данные для отображения (студента и занятие)
            var attendances = await _context.Attendances
                .Include(a => a.Student)
                .Include(a => a.Lesson)
                .ToListAsync();
            return View(attendances);
        }

        // GET: Attendance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var attendance = await _context.Attendances
                .Include(a => a.Student)
                .Include(a => a.Lesson)
                .FirstOrDefaultAsync(m => m.AttendanceId == id);

            if (attendance == null)
                return NotFound();

            return View(attendance);
        }

        // GET: Attendance/Create
        public IActionResult Create()
        {
            // Здесь можно подготовить данные для выпадающих списков (например, список студентов и занятий)
            return View();
        }

        // POST: Attendance/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttendanceId,StudentId,LessonId,WasPresent")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(attendance);
        }

        // GET: Attendance/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
                return NotFound();
            return View(attendance);
        }

        // POST: Attendance/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AttendanceId,StudentId,LessonId,WasPresent")] Attendance attendance)
        {
            if (id != attendance.AttendanceId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.AttendanceId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(attendance);
        }

        // GET: Attendance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var attendance = await _context.Attendances
                .Include(a => a.Student)
                .Include(a => a.Lesson)
                .FirstOrDefaultAsync(m => m.AttendanceId == id);
            if (attendance == null)
                return NotFound();

            return View(attendance);
        }

        // POST: Attendance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendances.Any(e => e.AttendanceId == id);
        }
    }
}
