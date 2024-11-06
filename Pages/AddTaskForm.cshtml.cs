using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;
using ToDoListPaczkowski.Data;
using ToDoListPaczkowski.Model;

namespace ToDoListPaczkowski.Pages
{
    public class AddTaskFormModel : PageModel
    {
        private readonly ILogger<AddTaskFormModel> _logger;
        private readonly ToDoListDbContext _context;

        public AddTaskFormModel(ILogger<AddTaskFormModel> logger, ToDoListDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<Priority> priorityList { get; set; }

        [BindProperty]
        public string name {  get; set; }
        [BindProperty]
        public string description { get; set; }
        [BindProperty]
        public Priority priority { get; set; }
        [BindProperty]
        public DateOnly? taskDate {  get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            priorityList = Enum.GetValues(typeof(Priority)).Cast<Priority>().ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            TaskToDo taskToDo = new TaskToDo(name, description, taskDate, priority);
            _context.taskToDo.Add(taskToDo);
            await _context.SaveChangesAsync();

            return Redirect("/Index");
        }
    }
}
