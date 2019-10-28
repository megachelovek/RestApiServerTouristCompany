using System.Collections.Generic;
using System.Threading.Tasks;
using ServerTouristCompanyApi.Models;

namespace ServerTouristCompanyApi.Services
{
    /// <summary>
    ///     Represents the set of methods for Transfer manipulation.
    /// </summary>
    public interface ITransferService
    {
        /// <summary>
        ///     Tries to create new Transfer.
        /// </summary>
        /// <param name="Transfer">Instance of <see cref="Transfer" /></param>
        /// <returns>Unique identifier.</returns>
        Task<int> Create(Transfer Transfer);

        /// <summary>
        ///     Tries to retrieve all Transfer objects.
        /// </summary>
        /// <returns>A collection of Transfer objects (collection might be empty, but never null).</returns>
        Task<IEnumerable<Transfer>> Get();

        /// <summary>
        ///     Tries to retrieve specified Transfer object if exists.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <returns>A <see cref="Transfer" /> object, or null.</returns>
        Task<Transfer> Get(int id);

        /// <summary>
        ///     Tries to perform update.
        /// </summary>
        /// <param name="Transfer">Instance of <see cref="Transfer" /> that holds values that we want updated.</param>
        /// <returns>An awaitable task.</returns>
        Task Update(Transfer Transfer);

        /// <summary>
        ///     Tries to delete specified Transfer.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <returns>An awaitable task.</returns>
        Task Delete(int id);
    }
}