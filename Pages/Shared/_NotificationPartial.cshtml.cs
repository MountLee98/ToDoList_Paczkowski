using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ToDoListPaczkowski.Data;

namespace ToDoListPaczkowski.Pages.Shared
{
    public class _NotificationPartialModel : PageModel
    {
        private readonly ILogger<_NotificationPartialModel> _logger;
        private readonly ToDoListDbContext _context;

        public _NotificationPartialModel(ILogger<_NotificationPartialModel> logger, ToDoListDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public int urgentCount { get; set; }
        public int highCount { get; set; }
        public int mediumCount { get; set; }
        public int lowCount { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            urgentCount = await _context.taskToDo.Where(u => u.Priority.Equals("Urgent")).CountAsync();
            return Page();
        }
    }
}
