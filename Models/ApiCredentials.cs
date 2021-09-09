using System.ComponentModel.DataAnnotations;

namespace Net5.Sample.API.Models
{
    /// <summary>
    /// Data Model to transfer API credentials to get JWT token
    /// </summary>
    public class ApiCredentials
    {
        /// <summary>
        /// API key
        /// </summary>
        [Required]
        public string ApiKey { get; set; }

        /// <summary>
        /// API Password to validate the API Key
        /// </summary>
        [Required]
        public string ApiSecret { get; set; }
    }
}
