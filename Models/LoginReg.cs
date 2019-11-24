using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class LoginReg
    {
        public User NewUser { get; set; }
        public LoginUser ReturnUser { get; set; }
    }
}