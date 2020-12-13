using Dapper;
using MultitenancyPostgres.Constants;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MultitenancyPostgres.DataLayer
{
    public interface IPermissionRepository
    {
        List<string> userPermission(int Id);
    }


    public class PermissionRepository : IPermissionRepository
    {


        public string connectionString = "";

        public PermissionRepository (string connectionString)
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

        public   List<string> userPermission(int Id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var data = dbConnection.QueryAsync<string>("SELECT a.permission FROM permissions a" +
                    " left join grouppermission b on a.id = b.permissionid" +
                    " right join users c on c.id = b.userid" +
                    " WHERE b.userid = @Id", new { Id = Id });
                dbConnection.Close();

                return data.Result.ToList();

            }
        }
    }
}
