namespace ExCodeDapperAPI.Models
{
    public class EmployeeCreate
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public int ManagerId { get; set; }

    }
}
