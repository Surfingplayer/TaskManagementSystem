using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models
{
    public class TaskNotes
    {
        public int NotesId { get; set; }
        public int TaskID { get; set; }
        public int EmpID { get; set; }
        public string Notes { get; set; }
        public float WorkHours { get; set; }
        public bool IsActive { get; set; }
       

    }
}
