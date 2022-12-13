namespace ExCodeDapperAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Department Department { get; set; } = new Department();        
        public Manager Manager { get; set; } = new Manager();

    }
}
