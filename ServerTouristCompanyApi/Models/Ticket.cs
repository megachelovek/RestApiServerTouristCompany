using System;

namespace ServerTouristCompanyApi.Models
{
    /// <summary>
    ///     Билет
    /// </summary>
    public class Ticket
    {
        /// <summary>
        ///     Дата отправления
        /// </summary>
        public DateTime DateTimeDeparture { get; set; }

        /// <summary>
        ///     Дата прибытия
        /// </summary>
        public DateTime DateTimeArrival { get; set; }

        /// <summary>
        ///     Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Место отправления
        /// </summary>
        public string PlaceDeparture { get; set; }

        /// <summary>
        ///     Место прибытия
        /// </summary>
        public string PlaceArrival { get; set; }
    }
}