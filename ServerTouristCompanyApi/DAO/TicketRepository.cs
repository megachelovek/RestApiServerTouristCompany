using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using ServerTouristCompanyApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ServerTouristCompanyApi.DAO
{
    public class TicketRepository
    {
        internal static IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection("User ID=postgres;Password=;Host=localhost;Port=5432;Database=transfers;Pooling=true;");
            }
        }

        public static async Task<int> Add(Ticket item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.ExecuteAsync("INSERT INTO ticket (\"DatetimeOfDepartureTicket\",\"DatetimeOfArrivalTicket\",\"PlaceOfDepartureTicket\",\"PointOfArrivalTicket\",\"DataTicket\") " +
                    "VALUES(@DatetimeOfDepartureTicket,@DatetimeOfArrivalTicket,@PlaceOfDepartureTicket,@PointOfArrivalTicket,@DataTicket)", item).Result;
            }

        }

        public static async Task<IEnumerable<Ticket>> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryAsync<Ticket>("SELECT * FROM ticket").Result;
            }
        }

        public static async Task<Ticket> GetByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryAsync<Ticket>("SELECT * FROM ticket WHERE \"Id\" = @Id", new { Id = id }).Result.FirstOrDefault();
            }
        }

        public static async Task<int> Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.ExecuteAsync("DELETE FROM ticket WHERE \"Id\"=@Id", new { Id = id }).Result;
            }
        }

        public static Task<int> Update(Ticket item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.ExecuteAsync("UPDATE ticket SET \"DatetimeOfDepartureTicket\" = @DatetimeOfDepartureTicket, " +
                    "\"DatetimeOfArrivalTicket\" = @DatetimeOfArrivalTicket," +
                    "\"PlaceOfDepartureTicket\" = @PlaceOfDepartureTicket," +
                    "\"PointOfArrivalTicket\" = @PointOfArrivalTicket," +
                    "\"DataTicket\" = @DataTicket WHERE \"Id\" = @Id", item);
            }
        }
    }
}
