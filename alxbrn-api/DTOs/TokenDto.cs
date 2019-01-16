using System;

namespace alxbrn_api.DTOs
{
    /// <summary>
    /// Class for stroing TokenDto objects
    /// </summary>
    public class TokenDto
    {
        /// <summary>
        /// Token of the specific TokenDto
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Expires of the specific TokenDto
        /// </summary>
        public DateTime Expires { get; set; }
    }
}