using System.ComponentModel.DataAnnotations;

namespace alxbrn_api.Models
{
    /// <summary>
    /// Class for storing Product objects
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Id of the specific Product
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Image of the specific Product
        /// </summary>
        public string Image { get; set; } = "placeholder.png";
        /// <summary>
        /// Title of the specific Product
        /// </summary>
        [Required]
        [MinLength(1), MaxLength(64)]
        public string Title { get; set; }
        /// <summary>
        /// Description of the specific Product
        /// </summary>
        [Required]
        [MinLength(16), MaxLength(4096)]
        public string Description { get; set; }
        /// <summary>
        /// Price of the specific Product
        /// </summary>
        [Required]
        public decimal Price { get; set; }
        /// <summary>
        /// Weight of the specific Product
        /// </summary>
        public decimal Weight { get; set; } = 0.0m;
        /// <summary>
        /// Amount of the specific Product
        /// </summary>
        [Required]
        public int Amount { get; set; }
        /// <summary>
        /// CategoryItemId of the specific Product
        /// </summary>
        [Required]
        public int CategoryItemId { get; set; }
        /// <summary>
        /// EmployeeId of the specific Product
        /// </summary>
        [Required]
        public int EmployeeId { get; set; }
    }
}