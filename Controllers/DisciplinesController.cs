using System.Linq;
using System.Threading.Tasks;
using ITMOAspNetMvcApp.Data;
using ITMOAspNetMvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITMOAspNetMvcApp.Controllers
{
    public class DisciplinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisciplinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Disciplines
        public async Task<IActionResult> Index()
        {
            // Подключаем информацию о преподавателе для отображения
            var disciplines = await _context.Disciplines
                .Include(d => d.Teacher)
                .ToListAsync();
            return View(disciplines);
        }

        // GET: Disciplines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var discipline = await _context.Disciplines
                .Include(d => d.Teacher)
                .FirstOrDefaultAsync(m => m.DisciplineId == id);
            if (discipline == null)
                return NotFound();

            return View(discipline);
        }

        // GET: Disciplines/Create
        public IActionResult Create()
        {
            // Можно подготовить список преподавателей, если требуется выбор из существующих
            return View();
        }

        // POST: Disciplines/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DisciplineId,Name,Description,TeacherId")] Discipline discipline)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discipline);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discipline);
        }

        // GET: Disciplines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var discipline = await _context.Disciplines.FindAsync(id);
            if (discipline == null)
                return NotFound();
            return View(discipline);
        }

        // POST: Disciplines/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DisciplineId,Name,Description,TeacherId")] Discipline discipline)
        {
            if (id != discipline.DisciplineId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discipline);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisciplineExists(discipline.DisciplineId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(discipline);
        }

        // GET: Disciplines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var discipline = await _context.Disciplines
                .Include(d => d.Teacher)
                .FirstOrDefaultAsync(m => m.DisciplineId == id);
            if (discipline == null)
                return NotFound();

            return View(discipline);
        }

        // POST: Disciplines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discipline = await _context.Disciplines.FindAsync(id);
            _context.Disciplines.Remove(discipline);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisciplineExists(int id)
        {
            return _context.Disciplines.Any(e => e.DisciplineId == id);
        }
    }
}
