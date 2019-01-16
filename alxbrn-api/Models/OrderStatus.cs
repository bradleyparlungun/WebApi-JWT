using System.ComponentModel.DataAnnotations;

namespace alxbrn_api.Models
{
    /// <summary>
    /// Class for storing OrderStatus objects
    /// </summary>
    public class OrderStatus
    {
        /// <summary>
        /// Id for the specific OrderStatus
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Name for the specific OrderStatus
        /// </summary>
        ///  [Required]
        [MinLength(1), MaxLength(16)]
        public string Name { get; set; }
    }
}