using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using ServerTouristCompanyApi.Binders;
using ServerTouristCompanyApi.Configuration;
using ServerTouristCompanyApi.Models;
using ServerTouristCompanyApi.Services;
using ServerTouristCompanyApi.SwaggerExamples;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Filters;

namespace ServerTouristCompanyApi.Controllers
{
    /// <summary>
    /// Tour controller.
    /// </summary>
    [Route("api/[controller]")]
    public class TourController : ControllerBase
    {
        private readonly ConnectionStrings _connectionStrings;
        private readonly ITourService _service;
        private readonly ILogger _logger;

        /// <summary>
        /// Creates new instance of <see cref="TourController"/>.
        /// </summary>
        /// <param name="connectionStrings">
        /// Instance of <see cref="IOptionsSnapshot{ConnectionStrings}"/> object that contains connection string.
        /// More information: https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.ioptionssnapshot-1?view=aspnetcore-2.1
        /// TODO: https://www.strathweb.com/2016/09/strongly-typed-configuration-in-asp-net-core-without-ioptionst/
        /// </param>
        /// <param name="service">Instance of <see cref="ITourService"/></param>
        /// <param name="logger"></param>
        public TourController(IOptionsSnapshot<ConnectionStrings> connectionStrings, ITourService service, ILogger<TourController> logger)
        {
            _connectionStrings = connectionStrings.Value ?? throw new ArgumentNullException(nameof(connectionStrings));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Tries to create a new Tour.
        /// </summary>
        /// <param name="Tour">Instance of <see cref="Tour"/>.</param>
        /// <response code="200">Tour created.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        [ProducesResponseType(typeof(int), 201)]
        [ProducesResponseType(500)]
        [SwaggerRequestExample(typeof(Tour), typeof(TourRequestExample))]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post([FromBody] Tour Tour)
        {
            var response = await _service.Create(Tour);

            return CreatedAtRoute("getById", new { id = response }, response);
        }

        /// <summary>
        /// Tries to create a new Tour file.
        /// </summary>
        /// <param name="Tour">Instance of <see cref="Tour"/>.</param>
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
        public async Task<IActionResult> PostFile([ModelBinder(BinderType = typeof(JsonModelBinder))] Tour Tour, IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.OpenReadStream().CopyToAsync(memoryStream);

                var path = Path.Combine(Path.GetTempPath(), file.FileName);
                await System.IO.File.WriteAllBytesAsync(path, memoryStream.ToArray());
            }

            var response = await _service.Create(Tour);

            return CreatedAtRoute("getById", new { id = response }, response);
        }

        /// <summary>
        /// Tries to retrieve all Tour objects.
        /// </summary>
        /// <response code="200">All available Tour objects retrieved.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet, ResponseCache(CacheProfileName = "default")]
        [ProducesResponseType(typeof(IEnumerable<Tour>), 200)]
        [SwaggerResponseExample(200, typeof(TourResponseExample))]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get()
        {
            var response = await _service.Get().ConfigureAwait(false);
            
            List<Tour>  tours = (List<Tour>)new TourListResponseExample().GetExamples();

            return Ok(tours);
        }

        /// <summary>
        /// Tries to retrieve specified Tour.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <response code="200">Tour successfully retrieved.</response>
        /// <response code="404">Specified Tour doesn't exist.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{id:int:min(1)}", Name = "getById")]
        [ProducesResponseType(typeof(Tour), 200)]
        [ProducesResponseType(404)]
        [SwaggerResponseExample(200, typeof(TourListResponseExample))]
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
        /// Tries to update the Tour.
        /// </summary>
        /// <param name="Tour">Instance of <see cref="Tour"/> that holds values that we want updated.</param>
        /// <response code="200">Tour updated successfully.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPatch]
        [SwaggerRequestExample(typeof(Tour), typeof(TourRequestExample))]
        [Authorize(AuthenticationSchemes =
            JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Patch([FromBody] Tour Tour)
        {
            await _service.Update(Tour).ConfigureAwait(false);

            return Ok();
        }

        /// <summary>
        /// Tires to delete specified Tour.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <response code="200">Tour deleted successfully.</response>
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