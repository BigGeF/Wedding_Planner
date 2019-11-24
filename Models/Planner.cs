using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class Planner
    {
        [Key]
        public int PlannerId { get; set; }
        public int UserId { get; set; }
        public int PlanId { get; set; }
        public User User { get; set; }
        public Plan Plan { get; set; }
    }
}