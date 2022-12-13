using Dapper;
using ExCodeDapperAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Transactions;

namespace ExCodeDapperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("exCodeConn");
        }

        private static async Task<IEnumerable<Employee>> GetAllEmployees(SqlConnection connection)
        {
            var data = await connection.QueryAsync<Employee, Department, Manager, Employee> (
                @"SELECT 
	                Employee.Id
                    ,Employee.FirstName
                    ,Employee.LastName
	                ,isNull(Department.Id, 0) as Id
	                ,isNull(Department.Name, 'N/A') as Name
                    ,isNull(Manager.Id, 0) as Id
	                ,isNull(Manager.FirstName, 'N/A') as FirstName
	                ,isNull(Manager.LastName, 'N/A') as LastName
                 FROM Employees Employee
                 LEFT JOIN Departments Department on Employee.DepartmentId = Department.Id
                 LEFT JOIN Managers Manager on Employee.ManagerId = Manager.Id"
                ,
                map:(emp, dept, mgr) => { emp.Department = dept; emp.Manager = mgr; return emp; }
                ,
                splitOn: "Id" 
                );

            return data;

        }


        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {
            using var connection = new SqlConnection(_connectionString);

            return Ok(await GetAllEmployees(connection));

        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int Id)
        {
            using var connection = new SqlConnection(_connectionString);
            var employee = await connection.QueryAsync < Employee, Department, Manager, Employee > (
                @"SELECT 
	                Employee.Id
                    ,Employee.FirstName
                    ,Employee.LastName
	                ,isNull(Department.Id, 0) as Id
	                ,isNull(Department.Name, 'N/A') as Name
                    ,isNull(Manager.Id, 0) as Id
	                ,isNull(Manager.FirstName, 'N/A') as FirstName
	                ,isNull(Manager.LastName, 'N/A') as LastName
                 FROM Employees Employee
                 LEFT JOIN Departments Department on Employee.DepartmentId = Department.Id
                 LEFT JOIN Managers Manager on Employee.ManagerId = Manager.Id
                 WHERE Employee.Id = @Id"
                ,
                map:(emp, dept, mgr) => { emp.Department = dept; emp.Manager = mgr; return emp; }
                ,
                splitOn: "Id"
                ,
                param: new { Id = Id }
                );
            
            if (employee.Count() == 0)
            {
                return NotFound($"The employee with an Id of {Id} could not be found.");
            }
            else
            {
                return Ok(employee);
            }

        }

        [HttpPost]
        public async Task<ActionResult<List<Employee>>> CreateEmployee(EmployeeCreate employee)
        {
            using var connection = new SqlConnection(_connectionString);
            var insertCount = await connection.ExecuteAsync(
                @"insert into Employees 
                (FirstName, LastName, DepartmentId, ManagerId) 
                values 
                (@FirstName, @LastName, @DepartmentId, @ManagerId)", employee);
            
            if (insertCount > 0)
            {
                return Ok(await GetAllEmployees(connection));
            }
            else
            {
                return BadRequest("There was a problem creating the employee.");
            }

        }

        [HttpPut]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(EmployeeRecord employee)
        {
            using var connection = new SqlConnection(_connectionString);
            var updateCount = await connection.ExecuteAsync(
                @"update Employees set 
                FirstName = @FirstName, 
                LastName = @LastName,
                DepartmentId = @DepartmentId,
                ManagerId = @ManagerId
                where Id = @Id", employee);
            if (updateCount > 0)
            {
                return Ok(await GetAllEmployees(connection));
            }
            else
            {
                return BadRequest($"There was a problem updating the employee with an Id of {employee.Id}.");
            }

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int Id)
        {
            using var connection = new SqlConnection(_connectionString);
            var deleteCount = await connection.ExecuteAsync("delete from Employees where Id = @Id",
                new { Id = Id });
            if (deleteCount > 0)
            {
                return Ok(await GetAllEmployees(connection));
            }
            else
            {
                return BadRequest($"There was a problem deleting the employee with an Id of {Id}.");
            }

        }
    }
}
