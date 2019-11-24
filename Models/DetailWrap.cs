using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class DetailWrap
    {
        public Plan ThisPlan { get; set; }
        public List<User> Guests { get; set; }
    }
}