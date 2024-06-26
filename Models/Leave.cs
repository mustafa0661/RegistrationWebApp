using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace RegistrationWebApp.Models
{
    public class Leave
    {
        [Key]
        public int Id {get; set;}

        [Required]
        public string UserId {get; set;}

        [ForeignKey("UserId")]
        public IdentityUser User {get; set;}

        [Required]
        public DateTime StartDate {get; set;}

        [Required]
        public DateTime EndDate {get; set;}
        public bool? IsApproved {get; set;}
        
        [NotMapped]
        public int TotalDays => (EndDate - StartDate).Days + 1;
    }
}