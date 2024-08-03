namespace TaskManagementSystem.Models
{
    public class Notes
    {
        public int NotesId { get; set; }
        public int TaskId { get; set;}
        public int EmpID { get; set; }
        public string TaskNotes { get; set; }
        public float WorkHours { get; set; }
        public bool IsActive { get; set; }

    }
}
