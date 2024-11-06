using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using ToDoListPaczkowski.Data;
using ToDoListPaczkowski.Model;

namespace ToDoListPaczkowski.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ToDoListDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, ToDoListDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public TaskToDo completedTaskEdit { get; set; }
        public List<TaskToDo> nextDayTasks { get; set; }
        public List<TaskToDo> tasks { get; set; }
        public int urgentCount { get; set; }
        public int highCount { get; set; }
        public int mediumCount { get; set; }
        public int lowCount { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateOnly? tasksDate { get; set; }
        [BindProperty]
        public int id { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            urgentCount = await _context.taskToDo.Where(d => d.Deadline >= DateOnly.FromDateTime(DateTime.Now) 
                && d.Deadline<= DateOnly.FromDateTime(DateTime.Now).AddDays(7))
                .Where(u => u.Priority.Equals(Priority.Pilny))
                .Where(i => i.IsCompleted == false).CountAsync();

            highCount = await _context.taskToDo.Where(d => d.Deadline >= DateOnly.FromDateTime(DateTime.Now)
                && d.Deadline <= DateOnly.FromDateTime(DateTime.Now).AddDays(7))
                .Where(u => u.Priority.Equals(Priority.Wysoki))
                .Where(i => i.IsCompleted == false).CountAsync();

            mediumCount = await _context.taskToDo.Where(d => d.Deadline >= DateOnly.FromDateTime(DateTime.Now) 
                && d.Deadline <= DateOnly.FromDateTime(DateTime.Now).AddDays(7))
                .Where(u => u.Priority.Equals(Priority.Œredni))
                .Where(i => i.IsCompleted == false).CountAsync();

            lowCount = await _context.taskToDo.Where(d => d.Deadline >= DateOnly.FromDateTime(DateTime.Now) 
                && d.Deadline <= DateOnly.FromDateTime(DateTime.Now).AddDays(7))
                .Where(u => u.Priority.Equals(Priority.Niski))
                .Where(i => i.IsCompleted == false).CountAsync();

            nextDayTasks = await _context.taskToDo.Where(d => d.Deadline == DateOnly.FromDateTime(DateTime.Now).AddDays(1))
                .Where(i => i.IsCompleted == false).ToListAsync();

            tasks = await _context.taskToDo.Where(d => d.Deadline == tasksDate).Where(i => i.IsCompleted == false).ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var result = await _context.taskToDo.FindAsync(id);

            if (result == null)
                return NotFound();

            _context.taskToDo.Remove(result);
            await _context.SaveChangesAsync();
            return Redirect("/Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {

            completedTaskEdit = await _context.taskToDo.Where(i => i.Id == id).FirstAsync();

            if (completedTaskEdit != null)
            {
                completedTaskEdit.IsCompleted = true;
            }

            await _context.SaveChangesAsync();

            return Redirect("/Index");
        }
    }
}
