using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ToDoListPaczkowski.Data;
using ToDoListPaczkowski.Model;

namespace ToDoListPaczkowski.Pages
{
    public class TaskEditModel : PageModel
    {
        private readonly ILogger<TaskEditModel> _logger;
        private readonly ToDoListDbContext _context;

        public TaskEditModel(ILogger<TaskEditModel> logger, ToDoListDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<Priority> priorityList { get; set; }

        public TaskToDo selectedTaskEdit { get; set; }
        [BindProperty]
        public int id2 { get; set; }
        [BindProperty]
        public string name { get; set; }
        [BindProperty]
        public string description { get; set; }
        [BindProperty]
        public Priority priority { get; set; }
        [BindProperty]
        public DateOnly taskDate { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            selectedTaskEdit = await _context.taskToDo.Where(i => i.Id == id || i.Id == id2).FirstAsync();
            description = selectedTaskEdit.Description;
            priorityList = Enum.GetValues(typeof(Priority)).Cast<Priority>().ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //TaskToDo updatedTask= new TaskToDo(name, description, taskDate, priority);
            selectedTaskEdit = await _context.taskToDo.Where(i => i.Id == id2).FirstAsync();
            
            if (selectedTaskEdit != null)
            {
                selectedTaskEdit.Name = name;
                selectedTaskEdit.Description = description;
                selectedTaskEdit.Deadline = taskDate;
                selectedTaskEdit.Priority = priority;
            }

            await _context.SaveChangesAsync();

            return Redirect("/Index");
        }
    }
}
