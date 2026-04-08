using Microsoft.AspNetCore.Mvc;
using MyToDoList.Data;     // <-- important: reference the Data namespace
using MyToDoList.Models;

namespace MyToDoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }


        // Show all ToDo items
        public IActionResult Index()
        {
            var items = _context.ToDoItems.ToList();
            return View("index", _context.ToDoItems.ToList());   // Pass list to the view
        }

        // Show form to add a new ToDo item
        public IActionResult AddToDo()
        {
            return View(new ToDoItem());
        }

        // Show details of a single ToDo item
        public IActionResult ToDoItem(int id)
        {
            var item = _context.ToDoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = _context.ToDoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.ToDoItems.Remove(item);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ToggleComplete(int id)
        {
            var item = _context.ToDoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            item.IsCompleted = !item.IsCompleted;
            _context.Update(item);
            _context.SaveChanges();

            return RedirectToAction(nameof(ToDoItem), new { id = id });
        }

        // Insert a new ToDo item
        [HttpPost]
        public IActionResult InsertToDoItem(ToDoItem item)
        {
            if (item is null) return BadRequest();

            if (ModelState.IsValid)
            {
                _context.ToDoItems.Add(item);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // Validation failed; return index with current list and errors
            var items = _context.ToDoItems.ToList();
            return View("Index", items);
        }
    }
}
