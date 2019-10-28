using System.Collections.Generic;
using System.Threading.Tasks;
using ServerTouristCompanyApi.Models;

namespace ServerTouristCompanyApi.Services
{
    /// <summary>
    ///     Represents the set of methods for Reservation manipulation.
    /// </summary>
    public interface IReservationService
    {
        /// <summary>
        ///     Tries to create new Reservation.
        /// </summary>
        /// <param name="Reservation">Instance of <see cref="Reservation" /></param>
        /// <returns>Unique identifier.</returns>
        Task<int> Create(Reservation Reservation);

        /// <summary>
        ///     Tries to retrieve all Reservation objects.
        /// </summary>
        /// <returns>A collection of Reservation objects (collection might be empty, but never null).</returns>
        Task<IEnumerable<Reservation>> Get();

        /// <summary>
        ///     Tries to retrieve specified Reservation object if exists.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <returns>A <see cref="Reservation" /> object, or null.</returns>
        Task<Reservation> Get(int id);

        /// <summary>
        ///     Tries to perform update.
        /// </summary>
        /// <param name="Reservation">Instance of <see cref="Reservation" /> that holds values that we want updated.</param>
        /// <returns>An awaitable task.</returns>
        Task Update(Reservation Reservation);

        /// <summary>
        ///     Tries to delete specified Reservation.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <returns>An awaitable task.</returns>
        Task Delete(int id);
    }
}