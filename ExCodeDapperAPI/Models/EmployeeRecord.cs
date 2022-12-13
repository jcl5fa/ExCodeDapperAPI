namespace ExCodeDapperAPI.Models
{
    public class EmployeeRecord
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public int ManagerId { get; set; }

    }
}
