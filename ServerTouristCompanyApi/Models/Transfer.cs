using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerTouristCompanyApi.Models
{
    /// <summary>
    ///     Трансфер
    /// </summary>
    public class Transfer
    {
        public Transfer() { }
        public Transfer(DateTime dateTimeNearTransfer, int id, string placeNearTransfer, Ticket ticket)
        {
            DateTimeNearTransfer = dateTimeNearTransfer;
            Id = id;
            PlaceNearTransfer = placeNearTransfer;
            Ticket = ticket;
        }

        /// <summary>
        ///     Дата и время встречи
        /// </summary>
        public DateTime DateTimeNearTransfer { get; set; }

        /// <summary>
        ///     Идентификатор
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        ///     Место встречи
        /// </summary>
        public string PlaceNearTransfer { get; set; }

        /// <summary>
        ///     Билет к этому трансферу
        /// </summary>
        [ForeignKey("ticketFK")]
        public Ticket Ticket { get; set; }
    }
}