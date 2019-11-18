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
                return new NpgsqlConnection("User ID=postgres;Password=1234;Host=localhost;Port=5432;Database=transfers;Pooling=true;");
            }
        }

        public static async Task<int> Add(Person item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                try
                {
                    return dbConnection.ExecuteAsync("INSERT INTO client (\"Login\",\"Password\", \"RegistrationTime\") " +
                        "VALUES(" + item.Login + "," + item.Password + ",'" + DateTime.Now + "')").Result;
                }
                catch (Exception)
                {
                    return 0;
                }
            }

        }

        public static async Task<IEnumerable<Person>> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryAsync<Person>("SELECT * FROM client WHERE NOT (\"Role\" = 'admin')").Result;
            }
        }

        public static async Task<Person> GetByID(string id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryAsync<Person>("SELECT * FROM client WHERE \"Login\" = @Id", new { Id = id }).Result.FirstOrDefault();
            }
        }

        public static async Task<int> Remove(string id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.ExecuteAsync("DELETE FROM client WHERE \"Login\"=@Id", new { Id = id }).Result;
            }
        }

        public static Task<int> Update(Person item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.ExecuteAsync("UPDATE client SET \"Name\" = @Name, \"Address\" = @Address, \"Phone\" = @Phone WHERE \"Login\" = @Login", item);
            }
        }
    }
}
