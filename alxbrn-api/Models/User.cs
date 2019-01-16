using System.ComponentModel.DataAnnotations;

namespace alxbrn_api.Models
{
    /// <summary>
    /// Class for storing User objects
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id of the specific User
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Email of the specific User
        /// </summary>
        [EmailAddress]
        [StringLength(128)]
        public string Email { get; set; }
        /// <summary>
        /// Username of the specific User
        /// </summary>
        [Required]
        [MinLength(6), MaxLength(32)]
        public string Username { get; set; }
        /// <summary>
        /// Password of the specific User
        /// </summary>
        [Required]
        [MinLength(8), MaxLength(64)]
        public string Password { get; set; }
        /// <summary>
        /// Firstname of the specific User
        /// </summary>
        [MinLength(1), MaxLength(256)]
        public string Firstname { get; set; }
        /// <summary>
        /// Lastname of the specific User
        /// </summary>
        /// [StringLength(64)]
        public string Lastname { get; set; }
        /// <summary>
        /// Address1 of the specific User
        /// </summary>
        [Required]
        [MinLength(1), MaxLength(256)]
        public string Address1 { get; set; }
        /// <summary>
        /// Address2 of the specific User
        /// </summary>
        [MinLength(1), MaxLength(256)]
        public string Address2 { get; set; }
        /// <summary>
        /// Phone of the specific User
        /// </summary>
        [Phone]
        [MinLength(6), MaxLength(16)]
        public string Phone { get; set; }
    }
}