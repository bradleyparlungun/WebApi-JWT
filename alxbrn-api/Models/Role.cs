using System.ComponentModel.DataAnnotations;

namespace alxbrn_api.Models
{
    /// <summary>
    /// Class for storing Role objects
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Id for the specific Role
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Name for the specific Role
        /// </summary>
        [Required]
        [MinLength(1), MaxLength(16)]
        public string Name { get; set; }
    }
}