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
        public async Task<IActionResult> Post([FromBody] Transfer Transfer)
        {
            var response = await _service.Create(Transfer);

            return CreatedAtRoute("getById", new {id = response}, response);
        }

        /// <summary>
        ///     Tries to create a new Transfer file.
        /// </summary>
        /// <param name="Transfer">Instance of <see cref="Transfer" />.</param>
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
            Transfer Transfer, IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.OpenReadStream().CopyToAsync(memoryStream);

                var path = Path.Combine(Path.GetTempPath(), file.FileName);
                await System.IO.File.WriteAllBytesAsync(path, memoryStream.ToArray());
            }

            var response = await _service.Create(Transfer);

            return CreatedAtRoute("getById", new {id = response}, response);
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
            var response = await _service.Get().ConfigureAwait(false);

            var Transfers = (List<Transfer>) new TransferListResponseExample().GetExamples();

            return Ok(Transfers);
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
            JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.Get(id).ConfigureAwait(false);

            if (response == null)
                return NotFound(id);

            return Ok(response);
        }

        /// <summary>
        ///     Tries to update the Transfer.
        /// </summary>
        /// <param name="Transfer">Instance of <see cref="Transfer" /> that holds values that we want updated.</param>
        /// <response code="200">Transfer updated successfully.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPatch]
        [SwaggerRequestExample(typeof(Transfer), typeof(TransferRequestExample))]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Patch([FromBody] Transfer Transfer)
        {
            await _service.Update(Transfer).ConfigureAwait(false);

            return Ok();
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
            await _service.Delete(id).ConfigureAwait(false);

            return Ok();
        }
    }
}