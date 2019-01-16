using System.ComponentModel.DataAnnotations;

namespace alxbrn_api.DTOs
{
    /// <summary>
    /// Class for storing AuthorizeRequestDto objects
    /// </summary>
    public class AuthorizeRequestDto
    {
        /// <summary>
        /// AppToken of the specific AuthorizeRequestDto
        /// </summary>
        [Required]
        [MinLength(32), MaxLength(32)]
        public string AppToken { get; set; }
        /// <summary>
        /// AppSecret of the specific AuthorizeRequestDto
        /// </summary>
        [Required]
        [MinLength(32), MaxLength(32)]
        public string AppSecret { get; set; }
    }
}