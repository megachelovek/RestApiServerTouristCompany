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
    public class TransferRepository
    {
        internal static IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection("User ID=postgres;Password=;Host=localhost;Port=5432;Database=transfers;Pooling=true;");
            }
        }

        public static async Task<int> Add(Transfer item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var ticket = await TicketRepository.GetByID(item.Ticket.Id);
                return dbConnection.ExecuteAsync("INSERT INTO transfer (\"DateTimeNearTransfer\",\"PlaceNearTransfer\",\"Ticket\") " +
                    "VALUES('" + item.DateTimeNearTransfer.Date.ToString() + "','" + item.PlaceNearTransfer + "'," + ticket.Id + ")").Result;
            }

        }

        public static async Task<IEnumerable<Transfer>> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var dbResult = await dbConnection.QueryAsync<Transfer, Ticket, KeyValuePair<Transfer, Ticket>>("SELECT " +
                    "transfer.\"Id\",transfer.\"DateTimeNearTransfer\",transfer.\"PlaceNearTransfer\", " +
                    "ticket.\"Id\", ticket.\"DatetimeOfDepartureTicket\", ticket.\"DatetimeOfArrivalTicket\",ticket.\"PlaceOfDepartureTicket\",ticket.\"PointOfArrivalTicket\",ticket.\"DataTicket\"" +
                    "FROM transfer LEFT JOIN ticket ON transfer.\"Ticket\" = ticket.\"Id\"",
                    (a, s) => new KeyValuePair<Transfer, Ticket>(a, s));
                var dict = dbResult.GroupBy(g => g.Key, g => g.Value).ToDictionary(g => g.Key, g => g.AsEnumerable());
                var result = dict.Select(x => new Transfer(x.Key.DateTimeNearTransfer, x.Key.Id, x.Key.PlaceNearTransfer, x.Value.First() == null ? null : new Ticket(
                    x.Value.First().DatetimeOfDepartureTicket, x.Value.First().DatetimeOfArrivalTicket, x.Value.First().Id, x.Value.First().PlaceOfDepartureTicket, x.Value.First().PointOfArrivalTicket, x.Value.First().DataTicket))).ToList();
                return result;
            }
        }

        public static async Task<Transfer> GetByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryAsync<Transfer>("SELECT * FROM transfer WHERE \"Id\" = @Id", new { Id = id }).Result.FirstOrDefault();
            }
        }

        public static async Task<int> Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.ExecuteAsync("DELETE FROM transfer WHERE \"Id\"=@Id", new { Id = id }).Result;
            }
        }

        public static Task<int> Update(Transfer item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.ExecuteAsync("UPDATE transfer SET \"DateTimeNearTransfer\" = @DateTimeNearTransfer, " +
                    "\"PlaceNearTransfer\" = @PlaceNearTransfer," +
                    "\"Ticket\" = @Ticket WHERE \"Id\" = @Id", new { item.Id, item.DateTimeNearTransfer, item.PlaceNearTransfer, Ticket = item.Ticket.Id });
            }
        }
    }
}
