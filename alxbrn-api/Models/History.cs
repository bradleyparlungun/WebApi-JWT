using System;
using System.ComponentModel.DataAnnotations;

namespace alxbrn_api.Models
{
    /// <summary>
    /// Class for storing History objects
    /// </summary>
    public class History
    {
        /// <summary>
        /// Id of the specific History
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Title of the specific History
        /// </summary>
        [Required]
        [MinLength(1), MaxLength(256)]
        public string Title { get; set; }
        /// <summary>
        /// Date of the specific History
        /// </summary>
        [Required]
        public DateTime Date { get; set; }
        /// <summary>
        /// EmployeeId of the specific History
        /// </summary>
        [Required]
        public int EmployeeId { get; set; }
        /// <summary>
        /// HistoryActionId of the specific History 
        /// </summary>
        [Required]
        public int HistoryActionId { get; set; }
    }
}