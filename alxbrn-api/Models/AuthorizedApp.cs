using System;
using System.ComponentModel.DataAnnotations;

namespace alxbrn_api.Models
{
    /// <summary>
    /// Class for storing AuthorizedApp objects
    /// </summary>
    public class AuthorizedApp
    {
        /// <summary>
        /// Id of the specific AuthorizedApp        
        /// </summary>
        [Key]
        public int AuthorizedAppId { get; set; }

        /// <summary>
        /// Name of the specific AuthorizedApp
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// AppToken of the specific AuthorizedApp
        /// </summary>
        [Required]
        [StringLength(32)]
        public string AppToken { get; set; }

        /// <summary>
        /// AppSecret of the specific AuthorizedApp
        /// </summary>
        [Required]
        [StringLength(32)]
        public string AppSecret { get; set; }

        /// <summary>
        /// TokenExpiration of the specific AuthorizedApp
        /// </summary>
        public DateTime TokenExpiration { get; set; }
    }
}