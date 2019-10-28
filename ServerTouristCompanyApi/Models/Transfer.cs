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
        public DateTime DateTimeMeet { get; set; }

        /// <summary>
        ///     Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Место встречи
        /// </summary>
        public string PlaceMeet { get; set; }

        /// <summary>
        ///     Билет к этому трансферу
        /// </summary>
        public int IdTicket { get; set; }
    }
}