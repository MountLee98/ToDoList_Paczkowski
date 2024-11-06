using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToDoListPaczkowski.Model
{
    public enum Priority
    {
        Pilny = 1,
        Wysoki = 2,
        Średni = 3,
        Niski = 4
    }
    public class TaskToDo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateOnly Deadline { get; set; }
        [Required]
        public Priority Priority { get; set; }
        [Required]
        public bool IsCompleted { get; set; }

        public TaskToDo()
        {
            Name = string.Empty;
            Description = string.Empty;
            Deadline = new DateOnly();
            Priority = Priority.Niski;
            IsCompleted = false;
        }

        public TaskToDo(string name, string description, DateOnly deadline, Priority priority)
        {
            Name = name;
            Description = description;
            Deadline = deadline;
            Priority = priority;
            IsCompleted = false;
        }

        public TaskToDo(string name, string description, DateOnly? deadline, Priority priority)
        {
            Name = name;
            Description = description;
            Deadline = (DateOnly)deadline;
            Priority = priority;
            IsCompleted = false;
        }
    }
}
