using Dapper;
using MultitenancyPostgres.Constants;
using MultitenancyPostgres.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MultitenancyPostgres.DataLayer
{
    public interface ITenantRepository
    {
        public void AddTenant(Tenant tenant);

        public Task<Tenant> FindTenantById(int Id);

        public Task<Tenant> FindTenantByName(string Name);

        public Task<Boolean> IfTenantExists(string Name);
    }


    public class TenantRepository:ITenantRepository
    {

        public string connectionString = "";

        public TenantRepository(string connectionString)
        {
            this.connectionString = (String.IsNullOrEmpty(connectionString) ? ApplicationConstants.HOST_DB_CONNECTION_STRING : $"host=127.0.0.1;port=5432;database={connectionString};user id=postgres;password=123");
        }

        internal System.Data.IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public void AddTenant(Tenant tenant)
        {
          
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO tenants (name,status) VALUES(@Name,@Status)", tenant);
                dbConnection.Close();


            }
        }

        public async Task<Tenant> FindTenantById(int Id)
        {
            Tenant tenant = new Tenant();
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var data = dbConnection.Query<Tenant>("SELECT * FROM tenants  WHERE id = @Id",new {Id = Id }).FirstOrDefault();
                dbConnection.Close();

                tenant = data;

            }

            return tenant;
        }

        public async Task<Tenant> FindTenantByName(string Name)
        {
            Tenant tenant = new Tenant();
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var data = dbConnection.Query<Tenant>("SELECT * FROM tenants  WHERE name = @Name", new { Name = Name }).FirstOrDefault();
                dbConnection.Close();

                tenant = data;

            }

            return tenant;
        }

        public async Task<bool> IfTenantExists(string Name)
        {
            Tenant tenant = new Tenant();
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var data = dbConnection.Query<Tenant>("SELECT * FROM tenants  WHERE name = @Name", new { Name = Name }).FirstOrDefault();
                dbConnection.Close();

                if (!data.Equals(null))
                {
                    return true;
                }


            }

            return false;
        }
    }
}
