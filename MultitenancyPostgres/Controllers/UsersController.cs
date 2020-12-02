using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MultitenancyPostgres.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultitenancyPostgres.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly string _connectionString;


        public UsersController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("HostConnection");

        }
         [HttpPost("add_user")]
        [Route("add_user")]
        public async Task<IActionResult> AddUser(User user)
        {

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                 connection.Open();

                //user.Id = await connection.QuerySingleAsync<int>($@"INSERT INTO [ApplicationUser] ([UserName], [NormalizedUserName], [Email],
                //[NormalizedEmail], [EmailConfirmed], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled])
                //VALUES (@{nameof(User.UserName)}, @{nameof(User.NormalizedUserName)}, @{nameof(User.Email)},
                //@{nameof(User.NormalizedEmail)}, @{nameof(User.EmailConfirmed)}, @{nameof(User.PasswordHash)},
                //@{nameof(User.PhoneNumber)}, @{nameof(User.PhoneNumberConfirmed)}, @{nameof(User.TwoFactorEnabled)});
                //SELECT CAST(SCOPE_IDENTITY() as int)", user);

                var sql = "INSERT INTO Users(UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash," +
                    "PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,) VALUES(@UserName, @pNormalizedUserName,@Email,@NormalizedEmail" +
                    "@EmailConfirmed,@PasswordHash,@PhoneNumber,@PhoneNumberConfirmed,@TwoFactorEnabled)";
                using var cmd = new NpgsqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("UserName", user.UserName);
                cmd.Parameters.AddWithValue("NormalizedUserName", user.NormalizedUserName);
                cmd.Parameters.AddWithValue("Email", user.Email);
                cmd.Parameters.AddWithValue("NormalizedEmail", user.NormalizedEmail);
                cmd.Parameters.AddWithValue("EmailConfirmed", user.EmailConfirmed);
                cmd.Parameters.AddWithValue("PasswordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("PhoneNumber", user.PhoneNumber);
                cmd.Parameters.AddWithValue("PhoneNumberConfirmed", user.PhoneNumberConfirmed);
                cmd.Parameters.AddWithValue("TwoFactorEnabled", user.TwoFactorEnabled);
                cmd.Prepare();
                cmd.ExecuteNonQuery();




            }

            return Ok();
        }
    }
}
