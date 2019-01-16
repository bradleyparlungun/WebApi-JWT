using System.ComponentModel.DataAnnotations;

namespace alxbrn_api.Models
{
    /// <summary>
    /// Class for storing Role HistoryAction
    /// </summary>
    public class HistoryAction
    {
        /// <summary>
        /// Id for the specific HistoryAction
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Name for the specific HistoryAction
        /// </summary>
        [Required]
        [MinLength(1), MaxLength(16)]
        public string Name { get; set; }
    }
}