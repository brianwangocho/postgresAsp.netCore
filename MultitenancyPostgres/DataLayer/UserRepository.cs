using Dapper;
using Microsoft.IdentityModel.Tokens;
using MultitenancyPostgres.Constants;
using MultitenancyPostgres.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MultitenancyPostgres.DataLayer
{
    public class UserRepository : IUserRepository
    {
        public string connectionString = "";


        public UserRepository(string connectionString)
        {
            this.connectionString = (String.IsNullOrEmpty(connectionString) ? ApplicationConstants.HOST_DB_CONNECTION_STRING: $"host=127.0.0.1;port=5432;database={connectionString};user id=postgres;password=123"); 
        }


        internal System.Data.IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }
        public void  AddUser(User user)
        {
            string password = user.Password;
            user.Password = BCrypt.Net.BCrypt.HashPassword(password);
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO users (email,password,status) VALUES(@Email,@Password,@Status)", user);
                dbConnection.Close();

               
            }
        }

        public async Task<LoginResponse> LoginUser(LoginRequest loginRequest)
        {

            LoginResponse loginResponse = new LoginResponse();
            string key = "edms_secret_key_12345"; //Secret key which will be used later during validation    
            var issuer = "http://edms.com";  //normally this will be your site URL    
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>();

            IDbConnection dbConnection = Connection;
            
                dbConnection.Open();
                var user = (User)dbConnection.Query<User>("SELECT * FROM users  WHERE email = @Email",loginRequest).FirstOrDefault();

                if(user == null)
                {
                    loginResponse.Message = "this user doesnt exist";
                    loginResponse.Status = "01";
                    dbConnection.Close();
                    
                }
                else if (BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
                {
                permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                permClaims.Add(new Claim("valid", "1"));
                permClaims.Add(new Claim(ClaimTypes.Role, "officer"));
                permClaims.Add(new Claim("userid", user.Id.ToString()));
                var token = new JwtSecurityToken(issuer, //Issure    
                    issuer,  //Audience    
                    permClaims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials);
                var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
                loginResponse.Message = "success";
                    loginResponse.Status = "00";
                    loginResponse.Token = new JwtSecurityTokenHandler().WriteToken(token);
                loginResponse.ExpiresOn = token.ValidTo;
                    dbConnection.Close();

                }
                else
                {
                    loginResponse.Message = "invalid  password";
                    loginResponse.Status = "01";
                    dbConnection.Close();

                }





            return loginResponse;



        }
    }
}
