using System.ComponentModel.DataAnnotations;

namespace alxbrn_api.Models
{
    /// <summary>
    /// Class for storing OrderLine objects
    /// </summary>
    public class OrderLine
    {
        /// <summary>
        /// Id of the specific OrderLine
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// UnitId of the specific OrderLine
        /// </summary>
        [Required]
        public decimal UnitId { get; set; }
        /// <summary>
        /// UnitPrice of the specific OrderLine
        /// </summary>
        [Required]
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// UnitAmount of the specific OrderLine
        /// </summary>
        [Required]
        public int UnitAmount { get; set; }
        /// <summary>
        /// UnitWeight of the specific OrderLine
        /// </summary>
        [Required]
        public decimal UnitWeight { get; set; }
        /// <summary>
        /// TotalPrice of the specific OrderLine
        /// </summary>
        [Required]
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// TotalWeight of the specific OrderLine
        /// </summary>
        [Required]
        public decimal TotalWeight { get; set; }
        /// <summary>
        /// OrderId of the specific OrderLine
        /// </summary>
        [Required]
        public int OrderId { get; set; }
    }
}