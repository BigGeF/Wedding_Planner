using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class Dashboard
    {
        public List<Plan> AllPlans { get; set; }
        public List<User> AllGuests { get; set; }
        public Planner Planner { get; set; }
        public int CurrentUserId { get; set; }
    }
}