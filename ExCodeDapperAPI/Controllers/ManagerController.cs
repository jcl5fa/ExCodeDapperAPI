using Dapper;
using ExCodeDapperAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ExCodeDapperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ManagerController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("exCodeConn");
        }

        private static async Task<IEnumerable<Manager>> GetAllManagers(SqlConnection connection)
        {
            return await connection.QueryAsync<Manager>("select * from Managers");

        }

        [HttpGet]
        public async Task<ActionResult<List<Manager>>> GetManagers()
        {
            using var connection = new SqlConnection(_connectionString);

            return Ok(await GetAllManagers(connection));

        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Manager>> GetManager(int Id)
        {
            using var connection = new SqlConnection(_connectionString);
            var manager = await connection.QueryFirstOrDefaultAsync<Manager>("select * from Managers where Id=@Id",
                new { Id = Id });
            if (manager == null)
            {
                return NotFound($"The manager with an Id of {Id} could not be found.");
            }
            else
            {
                return Ok(manager);
            }

        }

        [HttpPost]
        public async Task<ActionResult<List<Manager>>> CreateManager(ManagerCreate manager)
        {
            using var connection = new SqlConnection(_connectionString);
            var insertCount = await connection.ExecuteAsync("insert into Managers (FirstName, LastName) values (@FirstName, @LastName)", manager);
            if (insertCount > 0)
            {
                return Ok(await GetAllManagers(connection));
            }
            else
            {
                return BadRequest("There was a problem creating the manager.");
            }

        }

        [HttpPut]
        public async Task<ActionResult<List<Manager>>> UpdateManager(Manager manager)
        {
            using var connection = new SqlConnection(_connectionString);
            var updateCount = await connection.ExecuteAsync("update Managers set FirstName = @FirstName, LastName = @LastName where Id = @Id", manager);
            if (updateCount > 0)
            {
                return Ok(await GetAllManagers(connection));
            }
            else
            {
                return BadRequest($"There was a problem updating the manager with an Id of {manager.Id}.");
            }

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<Manager>>> DeleteManager(int Id)
        {
            using var connection = new SqlConnection(_connectionString);
            var deleteCount = await connection.ExecuteAsync("delete from Managers where Id = @Id",
                new { Id = Id });
            if (deleteCount > 0)
            {
                return Ok(await GetAllManagers(connection));
            }
            else
            {
                return BadRequest($"There was a problem deleting the manager with an Id of {Id}.");
            }

        }
    }
}
