using System.ComponentModel.DataAnnotations;

namespace alxbrn_api.Models
{
    /// <summary>
    /// Class for storing Comment objects
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Id of the specific Comment
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Title of the specific Comment
        /// </summary>
        [Required]
        [MinLength(4), MaxLength(32)]
        public string Title { get; set; }
        /// <summary>
        /// Description of the specific Comment
        /// </summary>
        [Required]
        [MinLength(16), MaxLength(4096)]
        public string Description { get; set; }
        /// <summary>
        /// Rating of the specific Comment
        /// </summary>
        [Required]
        public decimal Rating { get; set; }
        /// <summary>
        /// ProductId of the specific Comment
        /// </summary>
        [Required]
        public int ProductId { get; set; }
    }
}