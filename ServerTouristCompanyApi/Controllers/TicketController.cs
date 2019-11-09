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
            TicketRepository.Add(Ticket);
            return Ok();
        }

        /// <summary>
        ///     Tries to create a new Ticket file.
        /// </summary>
        /// <param name="Ticket">Instance of <see cref="Ticket" />.</param>
        /// <param name="file">A file content</param>
        /// <returns></returns>
        [HttpPost("content")]
        [ProducesResponseType(typeof(int), 201)]
        [ProducesResponseType(500)]
        [AddSwaggerFileUploadButton]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme)]
        // Since we are using custom model provider this post method doesn't support swagger request examples
        // I guess this can be coded to be supported, but I feel it will go beyond this template's boundaries.
        public async Task<IActionResult> PostFile([ModelBinder(BinderType = typeof(JsonModelBinder))]
            Ticket Ticket, IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.OpenReadStream().CopyToAsync(memoryStream);

                var path = Path.Combine(Path.GetTempPath(), file.FileName);
                await System.IO.File.WriteAllBytesAsync(path, memoryStream.ToArray());
            }

            var response = await _service.Create(Ticket);

            return CreatedAtRoute("getById", new {id = response}, response);
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
            var response = await _service.Get().ConfigureAwait(false);

            return Ok(TicketRepository.FindAll());
        }

        /// <summary>
        ///     Tries to retrieve specified Ticket.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <response code="200">Ticket successfully retrieved.</response>
        /// <response code="404">Specified Ticket doesn't exist.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{id:int:min(1)}", Name = "getByIdTicket")]
        [ProducesResponseType(typeof(Ticket), 200)]
        [ProducesResponseType(404)]
        [SwaggerResponseExample(200, typeof(TicketListResponseExample))]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.Get(id).ConfigureAwait(false);

            if (response == null)
                return NotFound(id);

            return Ok(response);
        }

        [HttpGet("/GetTicket")]
        [Authorize(AuthenticationSchemes =
           JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetTicket(int id)
        {
            var result = new Ticket(DateTime.Now, DateTime.Now, 1, "q", "2", "d");

            return Ok(result);
        }

        /// <summary>
        ///     Tries to update the Ticket.
        /// </summary>
        /// <param name="Ticket">Instance of <see cref="Ticket" /> that holds values that we want updated.</param>
        /// <response code="200">Ticket updated successfully.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPatch]
        [SwaggerRequestExample(typeof(Ticket), typeof(TicketRequestExample))]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Patch([FromBody] Ticket Ticket)
        {
            await _service.Update(Ticket).ConfigureAwait(false);

            return Ok();
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
            await _service.Delete(id).ConfigureAwait(false);

            return Ok();
        }
    }
}