using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class Plan
    {
        [Key]
        public int PlanId { get; set; }

        [Required]
        public string Title { get; set; }        

        [Required]
        public string Time { get; set; }
        [Required]
        public int Druation {get;set;}
        [Required]
        [DataType(DataType.Date)] 
        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; }
        
        public List<Planner> Guest { get; set; }
    }
}