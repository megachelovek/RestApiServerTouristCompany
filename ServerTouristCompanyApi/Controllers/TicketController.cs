using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    ///     Ticket controller.
    /// </summary>
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ConnectionStrings _connectionStrings;
        private readonly ILogger _logger;
        private readonly ITicketService _service;

        /// <summary>
        ///     Creates new instance of <see cref="TicketController" />.
        /// </summary>
        /// <param name="connectionStrings">
        ///     Instance of <see cref="IOptionsSnapshot{TOptions}" /> object that contains connection string.
        ///     More information:
        ///     https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.ioptionssnapshot-1?view=aspnetcore-2.1
        ///     TODO: https://www.strathweb.com/2016/09/strongly-typed-configuration-in-asp-net-core-without-ioptionst/
        /// </param>
        /// <param name="service">Instance of <see cref="ITicketService" /></param>
        /// <param name="logger"></param>
        public TicketController(IOptionsSnapshot<ConnectionStrings> connectionStrings, ITicketService service,
            ILogger<TicketController> logger)
        {
            _connectionStrings = connectionStrings.Value ?? throw new ArgumentNullException(nameof(connectionStrings));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        ///     Tries to create a new Ticket.
        /// </summary>
        /// <param name="Ticket">Instance of <see cref="Ticket" />.</param>
        /// <response code="200">Ticket created.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost(Name = "PostTicket")]
        [ProducesResponseType(typeof(int), 201)]
        [ProducesResponseType(500)]
        [SwaggerRequestExample(typeof(Ticket), typeof(TicketRequestExample))]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> Post([FromBody] Ticket Ticket)
        {
            var result = await TicketRepository.Add(Ticket);
            return Ok(result);
        }

        /// <summary>
        ///     Tries to retrieve all Ticket objects.
        /// </summary>
        /// <response code="200">All available Ticket objects retrieved.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet(Name = "GetTicket")]
        [ResponseCache(CacheProfileName = "default")]
        [ProducesResponseType(typeof(IEnumerable<Ticket>), 200)]
        [SwaggerResponseExample(200, typeof(TicketResponseExample))]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get()
        {
            var result = await TicketRepository.GetAll();

            return Ok(result);
        }

        [HttpGet("/GetTicket")]
        [Authorize(AuthenticationSchemes =
           JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetTicket(int id)
        {
            var result = await TicketRepository.GetByID(id);

            return Ok(result);
        }

        /// <summary>
        ///     Tires to delete specified Ticket.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <response code="200">Ticket deleted successfully.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("{id:int:min(1)}")]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            int result;
            try
            {
                result = await TicketRepository.Remove(id);
            }
            catch
            {
                return StatusCode(500);
            }

            return Ok(result);
        }

        /// <summary>
        ///     Tries to update a Ticket.
        /// </summary>
        /// <param name="Ticket">Instance of <see cref="Ticket" />.</param>
        /// <response code="200">Ticket created.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut]
        [ProducesResponseType(typeof(int), 201)]
        [ProducesResponseType(500)]
        [SwaggerRequestExample(typeof(Ticket), typeof(TicketRequestExample))]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> Update([FromBody] Ticket Ticket)
        {
            var result = await TicketRepository.Update(Ticket);
            return Ok(result);
        }
    }
}