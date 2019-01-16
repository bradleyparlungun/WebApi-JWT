using System;
using System.ComponentModel.DataAnnotations;

namespace alxbrn_api.Models
{
    /// <summary>
    /// Class for storing Order objects
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Id of the specific Order
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// TotalWCoupon of the specific Order
        /// </summary>
        [Required]
        public decimal TotalWCoupon { get; set; }
        /// <summary>
        /// TotalWOCoupon of the specific Order
        /// </summary>
        [Required]
        public decimal TotalWOCoupon { get; set; }
        /// <summary>
        /// TrackingNumber of the specific Order
        /// </summary>
        [MinLength(1), MaxLength(64)]
        public string TrackingNumber { get; set; } = "00000000000000000";
        /// <summary>
        /// CouponId of the specific Order
        /// </summary>
        public int CouponId { get; set; } = 0;
        /// <summary>
        /// Country of the specific Order
        /// </summary>
        [Required]
        [MinLength(1), MaxLength(1024)]
        public string Country { get; set; }
        /// <summary>
        /// Region of the specific Order
        /// </summary>
        [Required]
        [MinLength(1), MaxLength(1024)]
        public string Region { get; set; }
        /// <summary>
        /// Address of the specific Order
        /// </summary>
        [Required]
        [MinLength(1), MaxLength(1024)]
        public string Address { get; set; }
        /// <summary>
        /// OrderedDate of the specific Order
        /// </summary>  
        [Required]
        public DateTime OrderedDate { get; set; }
        /// <summary>
        /// EstimatedDate of the specific Order
        /// </summary>
        [Required]
        public DateTime EstimatedDate { get; set; }
        /// <summary>
        /// ShippedDate
        /// </summary>
        public DateTime ShippedDate { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// StatusId of the specific Order
        /// </summary>
        [Required]
        public int StatusId { get; set; }
        /// <summary>
        /// UserId of the specific Order
        /// </summary>
        [Required]
        public int UserId { get; set; }
    }
}