using Dapper;
using Npgsql;
using ServerTouristCompanyApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ServerTouristCompanyApi.DAO
{
    public class ClientRepository
    {
        internal static IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection("User ID=postgres;Password=;Host=localhost;Port=5432;Database=transfers;Pooling=true;");
            }
        }

        public static async Task<int> Add(Person item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                try
                {
                    Log.WriteDBLog("Add new client: INSERT INTO client (\"Login\",\"Password\", \"RegistrationTime\") " +
                        "VALUES(" + item.Login + "," + item.Password + ",'" + DateTime.Now + "')");
                    return dbConnection.ExecuteAsync("INSERT INTO client (\"Login\",\"Password\", \"RegistrationTime\") " +
                        "VALUES(" + item.Login + "," + item.Password + ",'" + DateTime.Now + "')").Result;
                }
                catch (Exception e)
                {
                    Log.WriteDBLog(e.Message);
                    return 0;
                }
            }

        }

        public static async Task<IEnumerable<Person>> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                Log.WriteDBLog("GetAll SELECT * FROM client WHERE NOT(\"Role\" = 'admin')");
                dbConnection.Open();
                return dbConnection.QueryAsync<Person>("SELECT * FROM client WHERE NOT (\"Role\" = 'admin')").Result;
            }
        }

        public static async Task<Person> GetByID(string id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                Log.WriteDBLog("GetByID: SELECT * FROM client WHERE \"Login\" = " + id);
                dbConnection.Open();
                return dbConnection.QueryAsync<Person>("SELECT * FROM client WHERE \"Login\" = @Id", new { Id = id }).Result.FirstOrDefault();
            }
        }

        public static async Task<int> Remove(string id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                Log.WriteDBLog("Remove: DELETE FROM client WHERE \"Login\"=" + id);
                return dbConnection.ExecuteAsync("DELETE FROM client WHERE \"Login\"=@Id", new { Id = id }).Result;
            }
        }

        public static Task<int> Update(Person item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                Log.WriteDBLog("Update: UPDATE client SET \"Name\" = " + item.Name + ", \"Address\" = " + item.Address +", \"Phone\" = " + item.Phone + " WHERE \"Login\" = " + item.Login);
                return dbConnection.ExecuteAsync("UPDATE client SET \"Name\" = @Name, \"Address\" = @Address, \"Phone\" = @Phone WHERE \"Login\" = @Login", item);
            }
        }
    }
}
