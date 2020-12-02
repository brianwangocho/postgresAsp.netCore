using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MultitenancyPostgres.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MultitenancyPostgres.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //https://markjohnson.io/articles/asp-net-core-identity-without-entity-framework
        //https://stackoverflow.com/questions/650098/how-to-execute-an-sql-script-file-using-c-sharp

        private readonly string _connectionString;


        public UsersController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("HostConnection");

        }

        internal System.Data.IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(_connectionString);
            }
        }

        [HttpPost("add_user")]
        [Route("add_user")]
        public async Task<IActionResult> AddUser(User user)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO users (email,password,status) VALUES(@Email,@Password,@Status)", user);
                dbConnection.Close();
            }





            return Ok();
        }

        [HttpGet("get_user")]
        [Route("get_user")]
        public async Task<IActionResult> GetUser()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var Users =  dbConnection.Query<User>("SELECT * FROM users");


                return Ok(Users);
            }




          
        }
    }
}
