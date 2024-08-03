namespace TaskManagementSystem.Models
{
    public class Task
    {
        public int TaskID { get; set; }
        public int DataBaseID { get; set; }
        public int CategoryID { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public string AssignedBy { get; set; }
        public int EmpID { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
