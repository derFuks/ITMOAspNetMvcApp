using System.Linq;
using System.Threading.Tasks;
using ITMOAspNetMvcApp.Data;
using ITMOAspNetMvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;

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

        // GET: Students/ExportToCsv
        public async Task<IActionResult> ExportToCsv()
        {
            var students = await _context.Students.ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine("student_id,first_name,last_name,total_attendance_score,exam_score");

            foreach (var s in students)
            {
                sb.AppendLine($"{s.StudentId},{s.FirstName},{s.LastName},{s.TotalAttendanceScore},{s.ExamScore}");
            }

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", "students_export.csv");
        }

        // GET: Students/ExportToXls
        public async Task<IActionResult> ExportToXls()
        {
            var students = await _context.Students.ToListAsync();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Students");
            worksheet.Cell(1, 1).Value = "student_id";
            worksheet.Cell(1, 2).Value = "first_name";
            worksheet.Cell(1, 3).Value = "last_name";
            worksheet.Cell(1, 4).Value = "total_attendance_score";
            worksheet.Cell(1, 5).Value = "exam_score";

            for (int i = 0; i < students.Count; i++)
            {
                var s = students[i];
                worksheet.Cell(i + 2, 1).Value = s.StudentId;
                worksheet.Cell(i + 2, 2).Value = s.FirstName;
                worksheet.Cell(i + 2, 3).Value = s.LastName;
                worksheet.Cell(i + 2, 4).Value = s.TotalAttendanceScore;
                worksheet.Cell(i + 2, 5).Value = s.ExamScore;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "students_export.xlsx");
        }

    }
}