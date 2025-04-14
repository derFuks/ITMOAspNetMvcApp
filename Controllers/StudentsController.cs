using System.Linq;
using System.Threading.Tasks;
using ITMOAspNetMvcApp.Data;
using ITMOAspNetMvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITMOAspNetMvcApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: Students
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.ToListAsync();
            return View(students);
        }
        
        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
                
            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
                return NotFound();
                
            return View(student);
        }
        
        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }
        
        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,FirstName,LastName,Email,TotalAttendanceScore,ExamScore")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }
        
        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
                
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound();
            return View(student);
        }
        
        // POST: Students/Edit/5
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, [Bind("StudentId,FirstName,LastName,Email,TotalAttendanceScore,ExamScore")] Student student)
        // {
        //     if (id != student.StudentId)
        //         return NotFound();
                
        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(student);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!_context.Students.Any(e => e.StudentId == student.StudentId))
        //                 return NotFound();
        //             else
        //                 throw;
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(student);
        // }
        
        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
                
            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
                return NotFound();
                
            return View(student);
        }

                    [HttpPost]
                    [ValidateAntiForgeryToken]
                    public async Task<IActionResult> Edit(int id, [Bind("StudentId,FirstName,LastName,Email,TotalAttendanceScore,ExamScore")] Student student)
                    {
                        if (id != student.StudentId)
                        {
                            return NotFound();
                        }
                        
                        // Добавляем логирование нового значения FirstName
                        System.Diagnostics.Debug.WriteLine($"Изменённое значение FirstName: {student.FirstName}");

                        if (!ModelState.IsValid)
                        {
                            // Выведем ошибки в отладочную консоль
                            foreach (var key in ModelState.Keys)
                            {
                                foreach (var error in ModelState[key].Errors)
                                {
                                    System.Diagnostics.Debug.WriteLine($"Ошибка для {key}: {error.ErrorMessage}");
                                }
                            }
                            // Если ModelState не валиден, метод вернёт ту же страницу с ошибками.
                            return View(student);
                        }
                        
                        try
                        {
                            _context.Update(student);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!_context.Students.Any(e => e.StudentId == student.StudentId))
                                return NotFound();
                            else
                                throw;
                        }
                        
                        return RedirectToAction(nameof(Index));
                    }




        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}