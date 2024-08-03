using TaskManagementSystem.Models;
using Task = TaskManagementSystem.Models.Task;

namespace TaskManagementSystem.ViewModel
{
    public class TaskInfoViewModel
    {
        public int NotesId { get; set; }
        public int TaskID { get; set; }
        public int EmpID { get; set; }
        public string Notes { get; set; }
        public double WorkHours { get; set; }
        public bool IsActive { get; set; }
        public Employee Employee { get; set; }
        public Task task { get; set; }
    }
}
