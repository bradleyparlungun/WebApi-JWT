using System.ComponentModel.DataAnnotations;

namespace alxbrn_api.Models
{
    /// <summary>
    /// Class for storing Coupon objects
    /// </summary>
    public class Coupon
    {
        /// <summary>
        /// Id of the specific Coupon
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Name of the specific Coupon
        /// </summary>
        [Required]
        [MinLength(6), MaxLength(12)]
        public string Name { get; set; }
        /// <summary>
        /// Amount of the specific Coupon
        /// </summary>
        public int Amount { get; set; } = 0;
        /// <summary>
        /// Percentage of the specific Coupon
        /// </summary>
        public int Percentage { get; set; } = 0;
        /// <summary>
        /// UsagesCount of the specific Coupon
        /// </summary>
        public int UsagesCount { get; set; } = 0;
        /// <summary>
        /// UsagesAllowed of the specific Coupon
        /// </summary>
        public int UsagesAllowed { get; set; } = 50;
    }
}