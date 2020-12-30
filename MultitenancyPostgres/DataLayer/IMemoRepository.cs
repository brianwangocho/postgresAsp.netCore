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
    interface IMemoRepository
    {
        public Task AddMemo(Memo memo);

        public Task UpdateMemo(Memo memo);


        public Task<Memo> FindById(int id);


        public List<Memo> Memos();
    }

    public class MemoRepository : IMemoRepository
    {

        public string connectionString = "";

        public MemoRepository(string connectionString)
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
        public async Task AddMemo(Memo memo)
        {

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
              await  dbConnection.ExecuteAsync("INSERT INTO memo (title,content,attachmenturl,createdon,modifiedon,contenttype) VALUES(@Title,@Content,@Attachmenturl,@CreatedOn,@ModifiedOn,@ContentType)",memo);
                dbConnection.Close();


            }
        }

        public async Task<Memo> FindById(int id)
        {
            Memo memo = new Memo();
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var data = dbConnection.Query<Memo>("SELECT * FROM memo  WHERE id = @Id", new { Id = id }).FirstOrDefault();
                dbConnection.Close();

                memo = data;

            }

            return memo;
        }

        public List<Memo> Memos()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var data = dbConnection.Query<Memo>("SELECT * FROM memo ORDER BY createdon  DESC ");
                dbConnection.Close();

                return data.ToList();

            }
        }

        public Task UpdateMemo(Memo memo)
        {
            return null;
        }
    }
}
