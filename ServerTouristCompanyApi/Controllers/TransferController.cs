using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServerTouristCompanyApi.Binders;
using ServerTouristCompanyApi.Configuration;
using ServerTouristCompanyApi.DAO;
using ServerTouristCompanyApi.Models;
using ServerTouristCompanyApi.Services;
using ServerTouristCompanyApi.SwaggerExamples;
using Swashbuckle.AspNetCore.Filters;

namespace ServerTouristCompanyApi.Controllers
{
    /// <summary>
    ///     Transfer controller.
    /// </summary>
    [Route("api/[controller]")]
    public class TransferController : ControllerBase
    {
        private readonly ConnectionStrings _connectionStrings;
        private readonly ILogger _logger;
        private readonly ITransferService _service;

        /// <summary>
        ///     Creates new instance of <see cref="TransferController" />.
        /// </summary>
        /// <param name="connectionStrings">
        ///     Instance of <see cref="IOptionsSnapshot{TOptions}" /> object that contains connection string.
        ///     More information:
        ///     https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.ioptionssnapshot-1?view=aspnetcore-2.1
        ///     TODO: https://www.strathweb.com/2016/09/strongly-typed-configuration-in-asp-net-core-without-ioptionst/
        /// </param>
        /// <param name="service">Instance of <see cref="ITransferService" /></param>
        /// <param name="logger"></param>
        public TransferController(IOptionsSnapshot<ConnectionStrings> connectionStrings, ITransferService service,
            ILogger<TransferController> logger)
        {
            _connectionStrings = connectionStrings.Value ?? throw new ArgumentNullException(nameof(connectionStrings));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        ///     Tries to create a new Transfer.
        /// </summary>
        /// <param name="Transfer">Instance of <see cref="Transfer" />.</param>
        /// <response code="200">Transfer created.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost(Name = "PostTransfer")]
        [ProducesResponseType(typeof(int), 201)]
        [ProducesResponseType(500)]
        [SwaggerRequestExample(typeof(Transfer), typeof(TransferRequestExample))]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> Post([FromBody] Transfer transfer)
        {
            var response = await TransferRepository.Add(transfer);

            return Ok(response);
        }

        /// <summary>
        ///     Tries to retrieve all Transfer objects.
        /// </summary>
        /// <response code="200">All available Transfer objects retrieved.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet(Name = "GetTransfer")]
        [ResponseCache(CacheProfileName = "default")]
        [ProducesResponseType(typeof(IEnumerable<Transfer>), 200)]
        [SwaggerResponseExample(200, typeof(TransferResponseExample))]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get()
        {
            var response = await TransferRepository.GetAll();

            return Ok(response);
        }

        /// <summary>
        ///     Tries to retrieve specified Transfer.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <response code="200">Transfer successfully retrieved.</response>
        /// <response code="404">Specified Transfer doesn't exist.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{id:int:min(1)}", Name = "getByIdTransfer")]
        [ProducesResponseType(typeof(Transfer), 200)]
        [ProducesResponseType(404)]
        [SwaggerResponseExample(200, typeof(TransferListResponseExample))]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await TransferRepository.GetByID(id);

            /*if (response == null)
                return NotFound(id);*/

            return Ok(response);
        }

        /// <summary>
        ///     Tires to delete specified Transfer.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <response code="200">Transfer deleted successfully.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("{id:int:min(1)}")]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            int result;
            try
            {
                result = await TransferRepository.Remove(id);
            }
            catch
            {
                return StatusCode(500);
            }

            return Ok(result);
        }

        /// <summary>
        ///     Tries to update a Transfer.
        /// </summary>
        /// <param name="transfer">Instance of <see cref="Transfer" />.</param>
        /// <response code="200">Ticket created.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut]
        [ProducesResponseType(typeof(int), 201)]
        [ProducesResponseType(500)]
        [SwaggerRequestExample(typeof(Ticket), typeof(TicketRequestExample))]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> Update([FromBody] Transfer transfer)
        {
            var result = await TransferRepository.Update(transfer);
            return Ok(result);
        }
    }
}