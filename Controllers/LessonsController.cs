using System.Linq;
using System.Threading.Tasks;
using ITMOAspNetMvcApp.Data;
using ITMOAspNetMvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITMOAspNetMvcApp.Controllers
{
    public class LessonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LessonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lessons
        public async Task<IActionResult> Index()
        {
            // Подключаем связанные данные для отображения (например, дисциплину)
            var lessons = await _context.Lessons.Include(l => l.Discipline).ToListAsync();
            return View(lessons);
        }

        // GET: Lessons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var lesson = await _context.Lessons
                .Include(l => l.Discipline)
                .FirstOrDefaultAsync(m => m.LessonId == id);
            if (lesson == null)
                return NotFound();

            return View(lesson);
        }

        // GET: Lessons/Create
        public IActionResult Create()
        {
            // Здесь можно подготовить список дисциплин для выбора
            return View();
        }

        // POST: Lessons/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LessonId,DisciplineId,LessonDate")] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lesson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lesson);
        }

        // GET: Lessons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
                return NotFound();
            return View(lesson);
        }

        // POST: Lessons/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LessonId,DisciplineId,LessonDate")] Lesson lesson)
        {
            if (id != lesson.LessonId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lesson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessonExists(lesson.LessonId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(lesson);
        }

        // GET: Lessons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var lesson = await _context.Lessons
                .Include(l => l.Discipline)
                .FirstOrDefaultAsync(m => m.LessonId == id);
            if (lesson == null)
                return NotFound();

            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LessonExists(int id)
        {
            return _context.Lessons.Any(e => e.LessonId == id);
        }
    }
}
