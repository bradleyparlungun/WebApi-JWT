using System.ComponentModel.DataAnnotations;

namespace alxbrn_api.Models
{
    /// <summary>
    /// Class for storing CategoryItem objects
    /// </summary>
    public class CategoryItem
    {
        /// <summary>
        /// Id of the specific CategoryItem
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Name of the specific CategoryItem
        /// </summary>
        [Required]
        [MinLength(1), MaxLength(32)]
        public string Name { get; set; }
        /// <summary>
        /// CategoryId of the specific CategoryItem
        /// </summary>
        [Required]
        public int CategoryId { get; set; }
    }
}