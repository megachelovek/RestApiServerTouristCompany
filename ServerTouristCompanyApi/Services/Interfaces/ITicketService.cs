using System.Collections.Generic;
using System.Threading.Tasks;
using ServerTouristCompanyApi.Models;

namespace ServerTouristCompanyApi.Services
{
    /// <summary>
    ///     Represents the set of methods for Ticket manipulation.
    /// </summary>
    public interface ITicketService
    {
        /// <summary>
        ///     Tries to create new Ticket.
        /// </summary>
        /// <param name="Ticket">Instance of <see cref="Ticket" /></param>
        /// <returns>Unique identifier.</returns>
        Task<int> Create(Ticket Ticket);

        /// <summary>
        ///     Tries to retrieve all Ticket objects.
        /// </summary>
        /// <returns>A collection of Ticket objects (collection might be empty, but never null).</returns>
        Task<IEnumerable<Ticket>> Get();

        /// <summary>
        ///     Tries to retrieve specified Ticket object if exists.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <returns>A <see cref="Ticket" /> object, or null.</returns>
        Task<Ticket> Get(int id);

        /// <summary>
        ///     Tries to perform update.
        /// </summary>
        /// <param name="Ticket">Instance of <see cref="Ticket" /> that holds values that we want updated.</param>
        /// <returns>An awaitable task.</returns>
        Task Update(Ticket Ticket);

        /// <summary>
        ///     Tries to delete specified Ticket.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <returns>An awaitable task.</returns>
        Task Delete(int id);
    }
}