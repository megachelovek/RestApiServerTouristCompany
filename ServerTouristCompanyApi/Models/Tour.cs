using System;

namespace ServerTouristCompanyApi.Models
{
    /// <summary>
    /// The Tour
    /// </summary>
    public class Tour
    {
        /// <summary>
        /// Gets the creation time.
        /// </summary>
        public DateTime CreateAd => DateTime.UtcNow;

        /// <summary>
        /// Gets or sets unique identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Tour value.
        /// </summary>
        public string Value { get; set; }
    }
}