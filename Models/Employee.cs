namespace TaskManagementSystem.Models
{
    public class Employee
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string EmpEmail { get; set; }
        public string EmpPassword { get; set; }
        public bool IsActive { get; set; }
    }
}
