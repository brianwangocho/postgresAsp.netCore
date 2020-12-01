using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MultitenancyPostgres.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MultitenancyPostgres.DataStore
{
    public class UserStore : IUserStore<User>, IUserEmailStore<User>, IUserPhoneNumberStore<User>, IUserTwoFactorStore<User>, IUserPasswordStore<User>
    {

        private readonly string _connectionString;

        public UserStore(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("HostConnection");
        }
        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

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
                cmd.Parameters.AddWithValue("NormalizedUserName",user.NormalizedUserName);
                cmd.Parameters.AddWithValue("Email",user.Email);
                cmd.Parameters.AddWithValue("NormalizedEmail",user.NormalizedEmail);
                cmd.Parameters.AddWithValue("EmailConfirmed",user.EmailConfirmed);
                cmd.Parameters.AddWithValue("PasswordHash",user.PasswordHash);
                cmd.Parameters.AddWithValue("PhoneNumber",user.PhoneNumber);
                cmd.Parameters.AddWithValue("PhoneNumberConfirmed",user.PhoneNumberConfirmed);
                cmd.Parameters.AddWithValue("TwoFactorEnabled",user.TwoFactorEnabled);
                cmd.Prepare();
                cmd.ExecuteNonQuery();




            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();


            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                var deleteQuery = "DELETE FROM Users WHERE Id = @Id";

                using var cmd = new NpgsqlCommand(deleteQuery, connection);
                cmd.Parameters.AddWithValue("Id", user.Id);

                cmd.Prepare();
                cmd.ExecuteNonQuery();





            }

            return IdentityResult.Success;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<string> IUserPasswordStore<User>.GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<string> IUserPhoneNumberStore<User>.GetPhoneNumberAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<bool> IUserPhoneNumberStore<User>.GetPhoneNumberConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<bool> IUserTwoFactorStore<User>.GetTwoFactorEnabledAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<bool> IUserPasswordStore<User>.HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task IUserPasswordStore<User>.SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task IUserPhoneNumberStore<User>.SetPhoneNumberAsync(User user, string phoneNumber, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task IUserPhoneNumberStore<User>.SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task IUserTwoFactorStore<User>.SetTwoFactorEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
