using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MultitenancyPostgres.DataLayer;
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

        internal System.Data.IDbConnection Connection1
        {
            get
            {
                return new NpgsqlConnection("Server = localhost; Port = 5432; User Id = postgres; Password = 123");
            }
        }

        [HttpPost("add_user")]
        [Route("add_user")]
        public async Task<IActionResult> AddUser(User user)
        {
            //using (IDbConnection dbConnection = Connection)
            //{
            //    dbConnection.Open();
            //    dbConnection.Execute("INSERT INTO users (email,password,status) VALUES(@Email,@Password,@Status)", user);
            //    dbConnection.Close();
            //}

            UserRepository useRepository = new UserRepository("isuzu");
            useRepository.AddUser(user);


            return Ok();
        }

        [HttpPost("login_user")]
        [Route("login_user")]
        public async Task<IActionResult> LoginUser(LoginRequest user)
        {
            //using (IDbConnection dbConnection = Connection)
            //{
            //    dbConnection.Open();
            //    dbConnection.Execute("INSERT INTO users (email,password,status) VALUES(@Email,@Password,@Status)", user);
            //    dbConnection.Close();
            //}

            UserRepository useRepository = new UserRepository("isuzu");
           var result = useRepository.LoginUser(user);


            return Ok(result.Result);
        }


        [HttpGet("get_user")]
        [Route("get_user")]
        [Authorize(Roles = "officer")]
        public async Task<IActionResult> GetUser()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var Users =  dbConnection.Query<User>("SELECT * FROM users");
                return Ok(Users);
            }
        }


        [HttpPut("deactivate_user")]
        [Route("deactivate_user")]
        public async Task<IActionResult> Updateser(User user)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE users SET status = @Status,  password  = @Password, email= @Email WHERE id = @Id", user);
                dbConnection.Close();

                return Ok() ;
            }
        }

        [HttpPut("create_database")]
        [Route("create_database")]
        public async Task<IActionResult> CreateDatabase(User user)
        {

            using (IDbConnection dbConnection = Connection1)
            {
                dbConnection.Open();
                dbConnection.Execute("CREATE DATABASE "+user.Name+" WITH OWNER = postgres ENCODING = 'UTF8'");
                dbConnection.Close();
            }

       

            var cs = $"host=127.0.0.1;port=5432;database={user.Name};user id=postgres;password=123";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;


            cmd.CommandText = @"create table users (
                            id SERIAL Primary Key,
                            email VARCHAR(100) NOT NULL,
                            password VARCHAR (100) NOT NULL,
                            status VARCHAR(100)  DEFAULT 0
                            );

                            create table roles(
                            id SERIAL Primary Key,
                            name VARCHAR(100) NOT NULL
                            );";

            cmd.ExecuteNonQuery();

            con.Dispose();
            con.Close();
            return Ok();
        }
    }
}
