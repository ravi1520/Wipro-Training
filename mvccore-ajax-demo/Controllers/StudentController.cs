using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvccore_ajax_demo.Models;

namespace mvccore_ajax_demo.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentContext _context;

        // Dependency Injection for DbContext
        public StudentController(StudentContext context)
        {
            _context = context;
        }

        // GET: Students
        public IActionResult Index()
        {
            var students = _context.Students.ToList();
            return View(students);
        }

        // Example: Get Students via AJAX (return JSON)
       [HttpPost]
public IActionResult GetStudents()
{
    var students = _context.Students.ToList();
    return Json(students);
}

[HttpPost]
public IActionResult AddStudent([FromBody] Student student)
{
    if (ModelState.IsValid)
    {
        _context.Students.Add(student);
        _context.SaveChanges();
        return Json(new { success = true, message = "Student added successfully!" });
    }
    return Json(new { success = false, message = "Validation failed." });
}


        // Example: Update Student
        [HttpPost]
        public IActionResult UpdateStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(student).State = EntityState.Modified; // ✅ fixed
                _context.SaveChanges();
                return Json(new { success = true, message = "Student updated successfully!" });
            }
            return Json(new { success = false, message = "Validation failed." });
        }

        // Example: Delete Student
        [HttpPost]
        public IActionResult DeleteStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
                return Json(new { success = true, message = "Student deleted successfully!" });
            }
            return Json(new { success = false, message = "Student not found." });
        }
    }
}
