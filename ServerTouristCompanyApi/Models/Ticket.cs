using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerTouristCompanyApi.Models
{
    /// <summary>
    ///     Билет
    /// </summary>
    public class Ticket
    {
        public Ticket(DateTime datetimeOfDepartureTicket, DateTime datetimeOfArrivalTicket, int id, string placeOfDepartureTicket, string pointOfArrivalTicket, string dataTicket)
        {
            DatetimeOfDepartureTicket = datetimeOfDepartureTicket;
            DatetimeOfArrivalTicket = datetimeOfArrivalTicket;
            Id = id;
            PlaceOfDepartureTicket = placeOfDepartureTicket;
            PointOfArrivalTicket = pointOfArrivalTicket;
            DataTicket = dataTicket;
        }

        public Ticket() { }

        /// <summary>
        ///     Дата отправления
        /// </summary>
        public DateTime DatetimeOfDepartureTicket { get; set; }

        /// <summary>
        ///     Дата прибытия
        /// </summary>
        public DateTime DatetimeOfArrivalTicket { get; set; }

        /// <summary>
        ///     Идентификатор
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        ///     Место отправления
        /// </summary>
        public string PlaceOfDepartureTicket { get; set; }

        /// <summary>
        ///     Место прибытия
        /// </summary>
        public string PointOfArrivalTicket { get; set; }

        /// <summary>
        ///     Данные билета
        /// </summary>
        public string DataTicket { get; set; }
    }
}