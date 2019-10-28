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
using ServerTouristCompanyApi.Models;
using ServerTouristCompanyApi.Services;
using ServerTouristCompanyApi.SwaggerExamples;
using Swashbuckle.AspNetCore.Filters;

namespace ServerTouristCompanyApi.Controllers
{
    /// <summary>
    ///     Reservation controller.
    /// </summary>
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ConnectionStrings _connectionStrings;
        private readonly ILogger _logger;
        private readonly IReservationService _service;

        /// <summary>
        ///     Creates new instance of <see cref="ReservationController" />.
        /// </summary>
        /// <param name="connectionStrings">
        ///     Instance of <see cref="IOptionsSnapshot{TOptions}" /> object that contains connection string.
        ///     More information:
        ///     https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.ioptionssnapshot-1?view=aspnetcore-2.1
        ///     TODO: https://www.strathweb.com/2016/09/strongly-typed-configuration-in-asp-net-core-without-ioptionst/
        /// </param>
        /// <param name="service">Instance of <see cref="IReservationService" /></param>
        /// <param name="logger"></param>
        public ReservationController(IOptionsSnapshot<ConnectionStrings> connectionStrings, IReservationService service,
            ILogger<ReservationController> logger)
        {
            _connectionStrings = connectionStrings.Value ?? throw new ArgumentNullException(nameof(connectionStrings));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        ///     Tries to create a new Reservation.
        /// </summary>
        /// <param name="Reservation">Instance of <see cref="Reservation" />.</param>
        /// <response code="200">Reservation created.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost(Name = "PostReservation")]
        [ProducesResponseType(typeof(int), 201)]
        [ProducesResponseType(500)]
        [SwaggerRequestExample(typeof(Reservation), typeof(ReservationRequestExample))]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post([FromBody] Reservation Reservation)
        {
            var response = await _service.Create(Reservation);

            return CreatedAtRoute("getById", new {id = response}, response);
        }

        /// <summary>
        ///     Tries to create a new Reservation file.
        /// </summary>
        /// <param name="Reservation">Instance of <see cref="Reservation" />.</param>
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
            Reservation Reservation, IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.OpenReadStream().CopyToAsync(memoryStream);

                var path = Path.Combine(Path.GetTempPath(), file.FileName);
                await System.IO.File.WriteAllBytesAsync(path, memoryStream.ToArray());
            }

            var response = await _service.Create(Reservation);

            return CreatedAtRoute("getById", new {id = response}, response);
        }

        /// <summary>
        ///     Tries to retrieve all Reservation objects.
        /// </summary>
        /// <response code="200">All available Reservation objects retrieved.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet(Name = "GetReservation")]
        [ResponseCache(CacheProfileName = "default")]
        [ProducesResponseType(typeof(IEnumerable<Reservation>), 200)]
        [SwaggerResponseExample(200, typeof(ReservationResponseExample))]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get()
        {
            var response = await _service.Get().ConfigureAwait(false);

            var Reservations = (List<Reservation>) new ReservationListResponseExample().GetExamples();

            return Ok(Reservations);
        }

        /// <summary>
        ///     Tries to retrieve specified Reservation.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <response code="200">Reservation successfully retrieved.</response>
        /// <response code="404">Specified Reservation doesn't exist.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{id:int:min(1)}", Name = "getByIdReservation")]
        [ProducesResponseType(typeof(Reservation), 200)]
        [ProducesResponseType(404)]
        [SwaggerResponseExample(200, typeof(ReservationListResponseExample))]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.Get(id).ConfigureAwait(false);

            if (response == null)
                return NotFound(id);

            return Ok(response);
        }

        /// <summary>
        ///     Tries to update the Reservation.
        /// </summary>
        /// <param name="Reservation">Instance of <see cref="Reservation" /> that holds values that we want updated.</param>
        /// <response code="200">Reservation updated successfully.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPatch]
        [SwaggerRequestExample(typeof(Reservation), typeof(ReservationRequestExample))]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Patch([FromBody] Reservation Reservation)
        {
            await _service.Update(Reservation).ConfigureAwait(false);

            return Ok();
        }

        /// <summary>
        ///     Tires to delete specified Reservation.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <response code="200">Reservation deleted successfully.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("{id:int:min(1)}")]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id).ConfigureAwait(false);

            return Ok();
        }
    }
}