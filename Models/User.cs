using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class User
    {
        [Key]
        public int userId { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name="Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be 8 charactors or longer")]
        public string Password { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<Planner> Wedding { get; set; }

        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name="Confirm Password")]
        public string ConfirmPw { get; set; }
    }
}