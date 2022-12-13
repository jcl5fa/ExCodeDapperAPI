using Dapper;
using ExCodeDapperAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Data.SqlClient;

namespace ExCodeDapperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("exCodeConn");
        }

        private static async Task<IEnumerable<Department>> GetAllDepartments(SqlConnection connection)
        {
            return await connection.QueryAsync<Department>("select * from Departments");

        }

        [HttpGet]
        public async Task<ActionResult<List<Department>>> GetDepartments()
        {            
            using var connection = new SqlConnection(_connectionString);

            return Ok(await GetAllDepartments(connection));

        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Department>> GetDepartment(int Id)
        {
            using var connection = new SqlConnection(_connectionString);
            var department = await connection.QueryFirstOrDefaultAsync<Department>("select * from Departments where Id=@Id",
                new { Id = Id });
            if (department == null)
            {
                return NotFound($"The department with an Id of {Id} could not be found.");
            }
            else
            {
                return Ok(department);
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<List<Department>>> CreateDepartment(DepartmentCreate department)
        {
            using var connection = new SqlConnection(_connectionString);
            var insertCount = await connection.ExecuteAsync("insert into Departments (Name) values (@Name)", department);
            if (insertCount > 0)
            {
                return Ok(await GetAllDepartments(connection));
            } else
            {
                return BadRequest("There was a problem creating the department.");
            }

        }

        [HttpPut]
        public async Task<ActionResult<List<Department>>> UpdateDepartment(Department department)
        {
            using var connection = new SqlConnection(_connectionString);
            var updateCount = await connection.ExecuteAsync("update Departments set Name = @Name where Id = @Id", department);
            if (updateCount > 0)
            {
                return Ok(await GetAllDepartments(connection));
            }
            else
            {
                return BadRequest($"There was a problem updating the department with an Id of {department.Id}.");
            }

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<Department>>> DeleteDepartment(int Id)
        {
            using var connection = new SqlConnection(_connectionString);
            var deleteCount = await connection.ExecuteAsync("delete from Departments where Id = @Id", 
                new { Id = Id });
            if (deleteCount > 0)
            {
                return Ok(await GetAllDepartments(connection));
            }
            else
            {
                return BadRequest($"There was a problem deleting the department with an Id of {Id}.");
            }

        }
    }
}
