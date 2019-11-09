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
                return new NpgsqlConnection("User ID=postgres;Password=1234;Host=localhost;Port=5432;Database=transfers;Pooling=true;");
            }
        }

        public static void Add(Ticket item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO ticket (\"DatetimeOfDepartureTicket\",\"DatetimeOfArrivalTicket\",\"PlaceOfDepartureTicket\",\"PointOfArrivalTicket\",\"DataTicket\") " +
                    "VALUES(@DatetimeOfDepartureTicket,@DatetimeOfArrivalTicket,@PlaceOfDepartureTicket,@PointOfArrivalTicket,@DataTicket)", item);
            }

        }

        public static IEnumerable<Ticket> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var a = dbConnection.Query<Ticket>("SELECT * FROM ticket");
                return a;
            }
        }

        public Ticket FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Ticket>("SELECT * FROM customer WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM customer WHERE Id=@Id", new { Id = id });
            }
        }

        public void Update(Ticket item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE customer SET name = @Name,  phone  = @Phone, email= @Email, address= @Address WHERE id = @Id", item);
            }
        }
    }
}
