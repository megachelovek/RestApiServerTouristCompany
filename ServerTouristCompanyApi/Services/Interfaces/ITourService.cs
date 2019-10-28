using System.Collections.Generic;
using System.Threading.Tasks;
using ServerTouristCompanyApi.Models;

namespace ServerTouristCompanyApi.Services
{
    /// <summary>
    ///     Represents the set of methods for Tour manipulation.
    /// </summary>
    public interface ITourService
    {
        /// <summary>
        ///     Tries to create new Tour.
        /// </summary>
        /// <param name="Tour">Instance of <see cref="Tour" /></param>
        /// <returns>Unique identifier.</returns>
        Task<int> Create(Tour Tour);

        /// <summary>
        ///     Tries to retrieve all Tour objects.
        /// </summary>
        /// <returns>A collection of Tour objects (collection might be empty, but never null).</returns>
        Task<IEnumerable<Tour>> Get();

        /// <summary>
        ///     Tries to retrieve specified Tour object if exists.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <returns>A <see cref="Tour" /> object, or null.</returns>
        Task<Tour> Get(int id);

        /// <summary>
        ///     Tries to perform update.
        /// </summary>
        /// <param name="Tour">Instance of <see cref="Tour" /> that holds values that we want updated.</param>
        /// <returns>An awaitable task.</returns>
        Task Update(Tour Tour);

        /// <summary>
        ///     Tries to delete specified Tour.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <returns>An awaitable task.</returns>
        Task Delete(int id);
    }
}