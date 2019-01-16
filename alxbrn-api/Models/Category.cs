using System.ComponentModel.DataAnnotations;

namespace alxbrn_api.Models
{
    /// <summary>
    /// Class for storing Category objects
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Id of the specific Category
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Name of the specific Category
        /// </summary>
        [Required]
        [MinLength(1), MaxLength(32)]
        public string Name { get; set; }
    }
}