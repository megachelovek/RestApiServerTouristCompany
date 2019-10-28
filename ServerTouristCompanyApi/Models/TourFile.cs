using Microsoft.AspNetCore.Http;

namespace ServerTouristCompanyApi.Models
{
    public class TourFile : Tour
    {
        /// <summary>
        ///     Gets or set the file content.
        /// </summary>
        public IFormFile File { get; set; }
    }
}