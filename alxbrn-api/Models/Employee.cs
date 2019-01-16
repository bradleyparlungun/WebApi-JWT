using System;
using System.ComponentModel.DataAnnotations;

namespace alxbrn_api.Models
{
    /// <summary>
    /// Class for storing Employee objects
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Id of the specific Employee
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Email of the specific Employee
        /// </summary>
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// Password of the specific Employee
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// Firstname of the specific Employee
        /// </summary>
        [Required]
        public string Firstname { get; set; }
        /// <summary>
        /// Lastname of the specific Employee
        /// </summary>
        [Required]
        public string Lastname { get; set; }
        /// <summary>
        /// HireDate of the specific Employee
        /// </summary>
        [Required]
        public DateTime HireDate { get; set; }
        /// <summary>
        /// Salary of the specific Employee
        /// </summary>
        [Required]
        public decimal Salary { get; set; }
        /// <summary>
        /// RoleId of the specific Employee
        /// </summary>
        [Required]
        public int RoleId { get; set; }
    }
}