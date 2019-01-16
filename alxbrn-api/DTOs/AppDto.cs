using System;

namespace alxbrn_api.DTOs
{
    /// <summary>
    /// Class for storing AppDto objects
    /// </summary>
    public class AppDto
    {
        /// <summary>
        /// Name of the specific AppDto
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Expeiration date of the specific AppDto
        /// </summary>
        public DateTime TokenExpiration { get; set; }
    }
}