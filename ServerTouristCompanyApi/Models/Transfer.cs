using System;

namespace ServerTouristCompanyApi.Models
{
    /// <summary>
    ///     Трансфер
    /// </summary>
    public class Transfer
    {
        /// <summary>
        ///     Дата и время встречи
        /// </summary>
        public DateTime DateTimeNearTransfer { get; set; }

        /// <summary>
        ///     Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Место встречи
        /// </summary>
        public string PlaceNearTransfer { get; set; }

        /// <summary>
        ///     Билет к этому трансферу
        /// </summary>
        public Ticket Ticket { get; set; }
    }
}